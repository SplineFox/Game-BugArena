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

    public class PlayerMovement
    {
        #region Fields
        private MovementState _state = default;
        private MovementState _previousState = default;

        private Projectile2D _projectile2D = default;
        private Vector2 _direction = Vector2.zero;
        private float _speed = 0f;
        #endregion

        #region Properties
        public MovementState State { get => _state; }
        public MovementState PreviousState { get => _previousState; }
        #endregion

        #region Delegates & Events
        public Action<MovementState> StateChanged = delegate { };
        #endregion

        #region Constructors
        public PlayerMovement(Projectile2D projectile2D)
        {
            _projectile2D = projectile2D;
            _projectile2D.IsKinematic = true;

            _previousState = MovementState.Idling;
            _state = MovementState.Idling;
        }
        #endregion

        #region Public Methods
        public void Start()
        {
            if (_state == MovementState.Idling)
            {
                SetState(MovementState.Moving);
            }
        }

        public void Stop()
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
            _direction = direction;

            if (_state == MovementState.Moving)
                UpdateVelocity();
        }
        #endregion

        #region Private Methods
        private void SetState(MovementState state)
        {
            if (_state == state)
                return;

            _previousState = _state;
            _state = state;
            StateChanged.Invoke(state);
        }

        private void UpdateVelocity()
        {
            var velocity = _direction * _speed;
            _projectile2D.SetVelocity(velocity);
        }
        #endregion
    }
}