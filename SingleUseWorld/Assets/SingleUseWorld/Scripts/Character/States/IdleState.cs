using SingleUseWorld.FSM;
using UnityEngine;

namespace SingleUseWorld
{
    public class IdleState : State<Player, PlayerEvents>
    {
        #region Fields
        private RigidbodyMovementComponent _movementComponent = default;
        private PlayerView _playerView = default;
        private float idleSpeed = 0;
        #endregion

        #region Constructors
        public IdleState(RigidbodyMovementComponent movementComponent, PlayerView playerView)
        {
            _movementComponent = movementComponent;
            _playerView = playerView;
        }
        #endregion

        #region Public Methods
        public override void OnEnter()
        {
            _movementComponent.SetSpeed(idleSpeed);
            _playerView.PlayIdleAnimation();
        }

        public override void OnTrigger(PlayerEvents eventType)
        {
            switch (eventType)
            {
                case PlayerEvents.MovementStarted:
                    _stateMachine.Switch<MoveState>();
                    break;
            }
        }
        #endregion
    }
}