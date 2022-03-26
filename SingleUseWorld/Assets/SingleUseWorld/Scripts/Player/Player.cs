using SingleUseWorld.FSM;
using UnityEngine;


namespace SingleUseWorld
{
    public sealed class Player : Projectile
    {
        #region Fields
        [SerializeField] private PlayerView _playerView = default;
        private StateMachine<Player, PlayerEvents> _stateMachine = default;
        #endregion

        #region Properties
        protected override ProjectileView _projectileView => _playerView;
        #endregion

        #region Protected Methods
        protected override void Start()
        {
            base.Start();

            var idleState = new IdleState(_playerView);
            var moveState = new MoveState(_playerView);

            _stateMachine = new StateMachine<Player, PlayerEvents>(this, idleState);
            _stateMachine.AddState(moveState);
        }

        protected override void Update()
        {
            base.Update();

            var deltaTime = Time.deltaTime;
            _stateMachine.OnUpdate(deltaTime);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            var fixedDeltaTime = Time.fixedDeltaTime;
            _stateMachine.OnFixedUpdate(fixedDeltaTime);
        }
        #endregion

        #region Public Methods
        public void StartMovement()
        {
            _stateMachine.Trigger(PlayerEvents.MovementStarted);
        }

        public void SetMovementDirection(Vector2 direction)
        {
            var speed = 5;
            _movement.SetVelocity(direction * speed);
            _playerView.SetDirectionParameter(direction);
        }

        public void StopMovement()
        {
            _movement.SetVelocity(Vector2.zero);
            _stateMachine.Trigger(PlayerEvents.MovementStopped);
        }
        #endregion

#if UNITY_EDITOR
        private void OnGUI()
        {
            ShowCurrentState();
        }

        private void ShowCurrentState()
        {
            if (_stateMachine == null)
                return;

            var contentText = _stateMachine.CurrentState.GetType().Name;
            var contentStyle = GUI.skin.GetStyle("label");
            contentStyle.fontSize = 16;
            contentStyle.contentOffset = new Vector2(16, 16);
            contentStyle.normal.textColor = Color.white;

            GUILayout.Label(contentText, contentStyle);
        }
#endif
    }
}