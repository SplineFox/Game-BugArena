using SingleUseWorld.FSM;
using UnityEngine;


namespace SingleUseWorld
{
    public class Player : MonoBehaviour
    {
        #region Fields
        [SerializeField] private PlayerView _actorView = default;

        private StateMachine<Player, PlayerEvents> _stateMachine = default;
        private RigidbodyMovementComponent _movementComponent = default;
        #endregion

        #region LifeCycle Methods
        private void Start()
        {
            _movementComponent = GetComponent<RigidbodyMovementComponent>();
            _movementComponent.OnInitialize();

            var idleState = new IdleState(_movementComponent, _actorView);
            var moveState = new MoveState(_movementComponent, _actorView);

            _stateMachine = new StateMachine<Player, PlayerEvents>(this, idleState);
            _stateMachine.AddState(moveState);
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _movementComponent.OnUpdate(deltaTime);
            _stateMachine.OnUpdate(deltaTime);
        }

        private void FixedUpdate()
        {
            var fixedDeltaTime = Time.fixedDeltaTime;
            _movementComponent.OnFixedUpdate(fixedDeltaTime);
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
            _movementComponent.SetDirection(direction);
            _actorView.SetDirectionParameter(direction);
        }

        public void StopMovement()
        {
            _stateMachine.Trigger(PlayerEvents.MovementStopped);
        }
        #endregion

        #region Private Methods
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