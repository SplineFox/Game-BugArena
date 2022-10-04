using SingleUseWorld.FSM;
using System;
using UnityEngine;


namespace SingleUseWorld
{
    public sealed class Player : BaseCharacter, IControllable, IGrabbable
    {
        #region Fields
        [SerializeField] private PlayerArmament _armament = default;
        [SerializeField] private PlayerBodyView _body = default;
        [SerializeField] private ShadowView _shadow = default;

        private PlayerSettings _settings;
        private PlayerHealth _health;
        private PlayerSpeed _speed;
        private PlayerGripHandler _gripHandler;
        #endregion

        #region LifeCycle Methods
        private void Update()
        {
            _gripHandler.Tick();
        }
        #endregion

        #region Public Methods
        public void OnCreate(PlayerSettings settings)
        {
            _settings = settings;
            _speed = new PlayerSpeed(_settings.SpeedSettings, _movement);
            _health = new PlayerHealth(_settings.HealthSettings, this);
            _gripHandler = new PlayerGripHandler(_settings.GripHandlerSettings, _speed, _health);

            _armament.Initialize(_settings.ArmamentSettings);
            _armament.StateChanged += OnArmamentStateChanged;
            _movement.StateChanged += OnMovementStateChanged;

            _body.ThrowStartFrameReached += OnThrowStartFrameReached;
            _body.ThrowEndFrameReached += OnThrowEndFrameReached;
        }

        public void OnDestroy()
        {
            _armament.StateChanged -= OnArmamentStateChanged;
            _movement.StateChanged -= OnMovementStateChanged;

            _body.ThrowStartFrameReached -= OnThrowStartFrameReached;
            _body.ThrowEndFrameReached -= OnThrowEndFrameReached;
        }

        void IControllable.StartMovement()
        {
            _movement.StartMovement();
        }

        void IControllable.SetMovementDirection(Vector2 direction)
        {
            _movement.SetDirection(direction);
            _body.SetFacingDirection(direction);
        }

        void IControllable.StopMovement()
        {
            _movement.SetDirection(Vector2.zero);
            _movement.StopMovement();
        }

        void IControllable.SetArmamentDirection(Vector2 direction)
        {
            _armament.SetDirection(direction);
        }

        void IControllable.Use()
        {
            if (_armament.State == ArmamentState.Unarmed)
                return;

            _movement.MovementAllowed = false;

            _body.SetFacingDirection(_armament.AimDirection);
            _body.PlayThrowAnimation();
        }

        void IControllable.Drop()
        {
            _armament.Drop();
        }

        void IGrabbable.Grab(float grabbingSlowDown, float grabbingDamagePerSecond)
        {
            _gripHandler.Grab(grabbingSlowDown, grabbingDamagePerSecond);
        }

        void IGrabbable.Release(float grabbingSlowDown, float grabbingDamagePerSecond)
        {
            _gripHandler.Release(grabbingSlowDown, grabbingDamagePerSecond);
        }
        #endregion

        #region Private Methods
        private void OnThrowStartFrameReached()
        {
            _armament.Use();
        }

        private void OnThrowEndFrameReached()
        {
            _movement.MovementAllowed = true;
            _armament.FinishThrowedState();

            if (_movement.MovementDirection.magnitude != 0f)
                _body.SetFacingDirection(_movement.FacingDirection);
        }

        private void OnMovementStateChanged(MovementState movementState)
        {
            switch (movementState)
            {
                case MovementState.Knocked:
                    _body.PlayKnockedAnimation();
                    _armament.Drop();
                    _armament.PickupAllowed = false;
                    break;
                default:
                    ResolveAnimation();
                    break;
            }
        }

        private void OnArmamentStateChanged(ArmamentState armamentState)
        {
            switch (armamentState)
            {
                case ArmamentState.Throwed:
                    break;
                default:
                    ResolveAnimation();
                    break;
            }
        }

        private void ResolveAnimation()
        {
            switch (_armament.State)
            {
                case ArmamentState.Unarmed:
                    _speed.ResetItemFactor();
                    ResolveUnarmedMovement();
                    break;
                case ArmamentState.Armed:
                    _speed.SetItemFactor(_armament.HeldItem.SpeedFactor);
                    ResolveArmedMovement();
                    break;
            }
        }

        private void ResolveUnarmedMovement()
        {
            switch (_movement.State)
            {
                case MovementState.Idling:
                    _body.PlayIdleUnarmedAnimation();
                    break;
                case MovementState.Moving:
                    _body.PlayMoveUnarmedAnimation();
                    break;
            }
        }

        private void ResolveArmedMovement()
        {
            switch (_movement.State)
            {
                case MovementState.Idling:
                    _body.PlayIdleArmedAnimation();
                    break;
                case MovementState.Moving:
                    _body.PlayMoveArmedAnimation();
                    break;
            }
        }
        #endregion
    }
}