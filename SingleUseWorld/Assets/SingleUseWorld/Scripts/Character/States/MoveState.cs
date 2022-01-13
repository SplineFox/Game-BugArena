using SingleUseWorld.FSM;
using UnityEngine;

namespace SingleUseWorld
{
    public class MoveState : State<Player, PlayerEvents>
    {
        #region Fields
        private RigidbodyMovementComponent _movementComponent = default;
        private PlayerView _playerView = default;
        private float moveSpeed = 3.5f;
        #endregion

        #region Constructors
        public MoveState(RigidbodyMovementComponent movementComponent, PlayerView playerView)
        {
            _movementComponent = movementComponent;
            _playerView = playerView;
        }
        #endregion

        #region Public Methods
        public override void OnEnter()
        {
            _movementComponent.SetSpeed(moveSpeed);
            _playerView.PlayMoveAnimation();
        }

        public override void OnTrigger(PlayerEvents eventType)
        {
            switch (eventType)
            {
                case PlayerEvents.MovementStopped:
                    _stateMachine.Switch<IdleState>();
                    break;
            }
        }
        #endregion
    }
}