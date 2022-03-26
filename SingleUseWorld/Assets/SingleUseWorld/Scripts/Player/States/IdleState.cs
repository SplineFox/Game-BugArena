using SingleUseWorld.FSM;
using UnityEngine;

namespace SingleUseWorld
{
    public class IdleState : State<Player, PlayerEvents>
    {
        #region Fields
        private PlayerView _playerView = default;
        private float idleSpeed = 0;
        #endregion

        #region Constructors
        public IdleState(PlayerView playerView)
        {
            _playerView = playerView;
        }
        #endregion

        #region Public Methods
        public override void OnEnter()
        {
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