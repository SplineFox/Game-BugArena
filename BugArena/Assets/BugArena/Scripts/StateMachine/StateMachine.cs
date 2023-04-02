using System;
using System.Collections.Generic;

namespace BugArena.FSM
{
    public class StateMachine<C, E>
    {
        #region Fields
        private C _context;
        private State<C, E> _currentState;
        private State<C, E> _previousState;
        private Dictionary<System.Type, State<C, E>> _states = new Dictionary<Type, State<C, E>>();
        #endregion

        #region Properties
        public State<C, E> CurrentState { get => _currentState; }
        public State<C, E> PreviousState { get => _previousState; }
        #endregion

        #region Delegates & Events
        public event Action StateChanged;
        #endregion

        #region Constructors
        public StateMachine(C context, State<C, E> initialState)
        {
            _context = context;

            AddState(initialState);
            _currentState = initialState;
            _currentState.OnEnter();
        }
        #endregion

        #region Public Methods
        public void AddState(State<C, E> state)
        {
            state.SetStateMachineAndContext(this, _context);
            _states[state.GetType()] = state;
        }

        public void OnUpdate(float deltaTime)
        {
            _currentState.OnUpdate(deltaTime);
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            _currentState.OnFixedUpdate(fixedDeltaTime);
        }

        public void Trigger(E eventType)
        {
            _currentState?.OnTrigger(eventType);
        }

        public void Switch<T>() where T : State<C, E>
        {
            var stateType = typeof(T);
            if (_currentState.GetType() == stateType)
                return;

            _currentState?.OnExit();
            _previousState = _currentState;
            _currentState = _states[stateType];
            _currentState.OnEnter();

            StateChanged?.Invoke();
        }
        #endregion
    }
}