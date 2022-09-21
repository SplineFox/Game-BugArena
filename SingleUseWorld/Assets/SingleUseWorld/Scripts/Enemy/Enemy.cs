using System;
using System.Collections;
using UnityEngine;

namespace SingleUseWorld
{
    public class Enemy : BaseCharacter, IPoolable
    {
        #region Fields
        [SerializeField] private EnemySight _sight = default;
        [SerializeField] private EnemyBodyView _body = default;
        [SerializeField] private ShadowView _shadow = default;

        private int _health = 100;
        private EnemySettings _settings;
        #endregion

        #region Public Methods
        public void OnCreate(EnemySettings settings)
        {
            _settings = settings;

            _sight.Initialize();
            _sight.StateChanged += OnSightStateChanged;

            _movement.StateChanged += OnMovementStateChanged;
            _movement.SetSpeed(_settings.WanderSpeed);

            _projectile.GroundCollision += OnGroundHit;
        }

        public void OnDestroy()
        {
            _movement.StateChanged -= OnMovementStateChanged;
            _sight.StateChanged -= OnSightStateChanged;
            _projectile.GroundCollision -= OnGroundHit;
        }

        void IPoolable.OnReset()
        {}

        public void Damage(int damageAmount, Vector2 damageDirection)
        {
            _health -= damageAmount;
            if (_health <= 0)
            {
                float horizontalSpeed = UnityEngine.Random.Range(2f,5f);
                float verticalSpeed = UnityEngine.Random.Range(2f, 5f);

                _movement.Knockback(damageDirection * horizontalSpeed, verticalSpeed);
                _sight.SightAllowed = false;
                
                var angle = (damageDirection.x > 0) ? -180f : 180f;
                _body.Rotate(angle, 1.2f);
                _body.SetFacingDirectionParameter(damageDirection);
                _body.ShowFlash(0.1f);
                elevator.height = 1f;
            }
        }
        #endregion

        #region Private Methods
        private void OnGroundHit()
        {
            if (_health <= 0f)
            {
                Destroy(gameObject);
            }
        }

        private void OnMovementStateChanged(MovementState movementState)
        {
            switch (movementState)
            {
                case MovementState.Knocked:
                    _body.PlayKnockedAnimation();
                    break;
                case MovementState.Idling:
                    _body.PlayIdleAnimation();
                    break;
                case MovementState.Moving:
                    ResolveMovementAnimation();
                    break;
            }
        }

        private void OnSightStateChanged(SightState sightState)
        {
            if (_movement.State == MovementState.Moving)
                ResolveMovementAnimation();

            StopAllCoroutines();
            switch (sightState)
            {
                case SightState.InSight:
                    StartCoroutine(Chase());
                    break;
                case SightState.OutSight:
                    StartCoroutine(Wander());
                    break;
            }
        }

        private void ResolveMovementAnimation()
        {
            switch (_sight.State)
            {
                case SightState.InSight:
                    _body.PlayChaseAnimation();
                    break;
                case SightState.OutSight:
                    _body.PlayWanderAnimation();
                    break;
            }
        }

        private IEnumerator Chase()
        {
            _movement.SetSpeed(_settings.ChaseSpeed);
            _movement.StartMovement();

            while (true)
            {
                _movement.SetDirection(_sight.DirectionToTarget);
                _body.SetFacingDirectionParameter(_movement.FacingDirection);
                yield return null;
            }
        }

        private IEnumerator Wander()
        {
            var direction = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
            _movement.SetDirection(direction);
            _body.SetFacingDirectionParameter(direction);

            _movement.SetSpeed(_settings.WanderSpeed);
            _movement.StartMovement();

            yield return new WaitForSeconds(_settings.WanderMovingTime);
            StartCoroutine(Idle());
        }

        private IEnumerator Idle()
        {
            _movement.StopMovement();

            yield return new WaitForSeconds(_settings.WanderIdlingTime);
            StartCoroutine(Wander());
        }
        #endregion
    }
}