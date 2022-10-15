using System;
using System.Collections;
using UnityEngine;

namespace SingleUseWorld
{
    public class Enemy : BaseCharacter, IPoolable
    {
        #region Fields
        [SerializeField] private EnemySight _sight = default;
        [SerializeField] private EnemyGrip _grip = default;
        [SerializeField] private EnemyBodyView _body = default;
        [SerializeField] private ShadowView _shadow = default;

        private EnemySettings _settings;
        private EnemyHealth _health;
        #endregion

        #region Public Methods
        public void OnCreate(EnemySettings settings)
        {
            _settings = settings;
            _health = new EnemyHealth(_settings.HealthSettings);

            _grip.Initialize(_settings.GripSettings);
            _sight.Initialize(_settings.SightSettings);

            _health.Died += OnDied;
            _movement.StateChanged += OnMovementStateChanged;
            _sight.StateChanged += OnSightStateChanged;
            _projectile.GroundCollision += OnGroundHit;

            _movement.SetSpeed(_settings.WanderSpeed);
        }

        public void OnDestroy()
        {
            _health.Died -= OnDied;
            _movement.StateChanged -= OnMovementStateChanged;
            _sight.StateChanged -= OnSightStateChanged;
            _projectile.GroundCollision -= OnGroundHit;
        }

        void IPoolable.OnReset()
        {}

        public void TakeDamage(Damage damage)
        {
            _health.TakeDamage(damage);
        }
        #endregion

        #region Private Methods
        private void OnDied(Damage damage)
        {
            _sight.SightAllowed = false;
            _grip.GripAllowed = false;

            _movement.Knockback(damage.horizontalKnockback, damage.verticalKnockback);
            _body.SetFacingDirection(damage.direction);
            _body.StartSpin(damage.spinKnockback);
            _body.ShowFlash(0.1f);
        }

        private void OnGroundHit()
        {
            if (_health.IsDead)
                Destroy(gameObject);
        }

        private void OnMovementStateChanged(MovementState movementState)
        {
            switch (movementState)
            {
                case MovementState.Knocked:
                    _body.PlayKnockedAnimation();
                    elevator.height = _settings.KnockbackInitialHeight;
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
                _body.SetFacingDirection(_movement.FacingDirection);
                yield return null;
            }
        }

        private IEnumerator Wander()
        {
            var direction = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
            _movement.SetDirection(direction);
            _body.SetFacingDirection(direction);

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