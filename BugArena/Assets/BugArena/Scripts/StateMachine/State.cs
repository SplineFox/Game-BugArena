namespace BugArena.FSM
{
    public abstract class State<T,E>
    {
        #region Fields
        protected T _context;
        protected StateMachine<T,E> _stateMachine;
        #endregion

        #region Constructors
        public State()
        { }
        #endregion

        #region Public Methods
        public virtual void OnInitialize()
        { }

        public virtual void OnEnter()
        { }

        public virtual void OnUpdate(float deltaTime)
        { }

        public virtual void OnFixedUpdate(float fixedDeltaTime)
        { }

        public virtual void OnExit()
        { }

        public virtual void OnTrigger(E eventType)
        { }
        #endregion

        #region Internal Methods
        internal void SetStateMachineAndContext(StateMachine<T,E> stateMachine, T context)
        {
            _stateMachine = stateMachine;
            _context = context;
            OnInitialize();
        }
        #endregion
    }
}