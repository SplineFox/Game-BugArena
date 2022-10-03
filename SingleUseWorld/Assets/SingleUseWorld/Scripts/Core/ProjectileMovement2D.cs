using System;
using UnityEngine;

namespace SingleUseWorld
{
    public enum MovementState
    {
        Idling,
        Moving,
        Knocked
    }

    [RequireComponent(typeof(Projectile2D))]
    public sealed class ProjectileMovement2D : BaseComponent<MovementState>
    {
        #region Fields
        [SerializeField]
        private bool _movementAllowed = true;

        private Projectile2D _projectile2D = default;

        private Vector2 _facingDirection = Vector2.right;
        private Vector2 _movementDirection = Vector2.zero;
        private float _speed = 0f;
        #endregion

        #region Properties
        public Vector2 FacingDirection { get => _facingDirection; }
        public Vector2 MovementDirection { get => _movementDirection; }
        public bool MovementAllowed 
        { 
            get => _movementAllowed;
            set
            {
                _movementAllowed = value;
                if (_movementAllowed)
                    TryContinueMovement();
                else
                    StopMovement();
            }
        }
        #endregion

        #region Public Methods
        public void Initialize()
        {
            _projectile2D = GetComponent<Projectile2D>();
            _projectile2D.IsKinematic = true;

            _state = MovementState.Idling;
        }

        public void StartMovement()
        {
            if (_state == MovementState.Idling && _movementAllowed)
            {
                UpdateVelocity();
                SetState(MovementState.Moving);
            }
        }

        public void StopMovement()
        {
            if (_state == MovementState.Moving)
            {
                _projectile2D.ResetVelocity();
                SetState(MovementState.Idling);
            }
        }

        public void Knockback(Vector2 horizontalVelocity, float verticalVelocity = 0f)
        {
            if (_state == MovementState.Knocked)
                return;

            _projectile2D.IsKinematic = false;
            _projectile2D.SetVelocity(horizontalVelocity, verticalVelocity);
            SetState(MovementState.Knocked);
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;

            if (_state == MovementState.Moving)
                UpdateVelocity();
        }

        public void SetDirection(Vector2 direction)
        {
            _movementDirection = direction;
            UpdateFacingDirection();

            if (_state == MovementState.Moving)
                UpdateVelocity();
        }
        #endregion

        #region Private Methods
        private void TryContinueMovement()
        {
            if (HasMovementDirection())
                StartMovement();
        }

        private void UpdateFacingDirection()
        {
            if (HasMovementDirection())
                _facingDirection = MovementDirection;
        }

        private void UpdateVelocity()
        {
            var velocity = MovementDirection * _speed;
            _projectile2D.SetVelocity(velocity);
        }

        private bool HasMovementDirection()
        {
            return MovementDirection.magnitude > 0f;
        }
        #endregion
    }
}