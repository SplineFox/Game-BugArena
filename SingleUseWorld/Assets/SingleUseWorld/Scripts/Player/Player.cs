using SingleUseWorld.FSM;
using UnityEngine;


namespace SingleUseWorld
{
    public sealed class Player : MonoBehaviour, IControllable
    {
        #region Fields
        [SerializeField] private PlayerBodyView _body = default;
        [SerializeField] private ShadowView _shadow = default;

        private PlayerMovement _movement = default;
        private Projectile2D _projectile2D = default;
        #endregion

        #region Public Methods
        public void Initialize()
        {
            _projectile2D = GetComponent<Projectile2D>();
            _movement = new PlayerMovement(_projectile2D);
            _movement.SetSpeed(3.5f);

            _movement.StateChanged += OnMovementStateChanged;
        }

        void IControllable.StartMovement()
        {
            _movement.Start();
        }

        void IControllable.SetMovementDirection(Vector2 direction)
        {
            _movement.SetDirection(direction);
            _body.SetDirectionParameter(direction);
        }

        void IControllable.StopMovement()
        {
            _movement.Stop();
        }

        void IControllable.Use()
        {
        }

        void IControllable.Drop()
        {
        }
        #endregion

        #region Private Methods
        private void OnMovementStateChanged(MovementState state)
        {
            switch (state)
            {
                case MovementState.Idling:
                    _body.PlayIdleUnarmedAnimation();
                    break;
                case MovementState.Moving:
                    _body.PlayMoveUnarmedAnimation();
                    break;
                case MovementState.Knocked:
                    _body.PlayThrowAnimation();
                    break;
            }
        }
        #endregion
    }
}