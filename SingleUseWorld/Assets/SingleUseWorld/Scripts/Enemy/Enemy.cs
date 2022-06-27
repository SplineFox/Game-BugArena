using System;
using System.Collections;
using UnityEngine;

namespace SingleUseWorld
{
    public class Enemy : BaseCharacter
    {
        #region Fields
        [SerializeField] private EnemySight _sight = default;
        [SerializeField] private EnemyBodyView _body = default;
        [SerializeField] private ShadowView _shadow = default;

        private float _wanderSpeed = 1f;
        private float _chaseSpeed = 3f;

        private float _wanderMovingTime = 1f;
        private float _wanderIdlingTime = 1f;
        #endregion

        private void Start()
        {
            Initialize();
        }

        #region Public Methods
        public override void Initialize()
        {
            base.Initialize();

            _sight.Initialize();
            _sight.StateChanged += OnSightStateChanged;

            _movement.StateChanged += OnMovementStateChanged;
            _movement.SetSpeed(_wanderSpeed);
        }
        #endregion

        #region Private Methods
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
            _movement.SetSpeed(_chaseSpeed);
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

            _movement.SetSpeed(_wanderSpeed);
            _movement.StartMovement();

            yield return new WaitForSeconds(_wanderMovingTime);
            StartCoroutine(Idle());
        }

        private IEnumerator Idle()
        {
            _movement.StopMovement();

            yield return new WaitForSeconds(_wanderIdlingTime);
            StartCoroutine(Wander());
        }
        #endregion
    }
}