using SingleUseWorld.FSM;
using System;
using UnityEngine;


namespace SingleUseWorld
{
    public sealed class Player : MonoBehaviour, IControllable
    {
        #region Fields
        [SerializeField] private PlayerBodyView _body = default;
        [SerializeField] private ShadowView _shadow = default;
        
        [SerializeField] private PlayerArmament _armament = default;
        [SerializeField] private PlayerMovement _movement = default;

        private Projectile2D _projectile2D = default;
        #endregion

        #region Public Methods
        public void Initialize()
        {
            _projectile2D = GetComponent<Projectile2D>();

            _armament.Initialize(this);
            _armament.StateChanged += OnArmamentStateChanged;

            _movement.Initialize(this);
            _movement.StateChanged += OnMovementStateChanged;
            
            _movement.SetSpeed(4f);

            _body.ThrowStartFrameReached += OnThrowStartFrameReached;
            _body.ThrowEndFrameReached += OnThrowEndFrameReached;
        }

        void IControllable.StartMovement()
        {
            _movement.StartMovement();
        }

        void IControllable.SetMovementDirection(Vector2 direction)
        {
            _movement.SetDirection(direction);
            _body.SetFacingDirectionParameter(direction);
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
            _body.PlayThrowAnimation();
        }

        void IControllable.Drop()
        {
            _armament.Drop();
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
                    ResolveUnarmedMovement();
                    break;
                case ArmamentState.Armed:
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