using SingleUseWorld.StateMachine.EditorTime;

namespace SingleUseWorld.StateMachine.RunTime
{
    /// <summary>
    /// Represents the implementation of the State pattern.
    /// </summary>
    public class State : IStateLifecycle
    {
        #region Fields
        internal StateModel _originModel;

        internal Action[] _actions;
        internal Transition[] _transitions;
        #endregion

        #region Constructors
        internal State() { }

        public State(Action[] actions, Transition[] transitions)
        {
            _actions = actions;
            _transitions = transitions;
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Called by <see cref="StateRunner"/> when initializing state.
        /// </summary>
        /// <param name="stateRunner">
        /// State machine this instance belongs to.
        /// </param>
        public void OnInitState(StateRunner stateRunner)
        {
            void OnInitStateComponent(IStateLifecycle[] components)
            {
                foreach (var component in components)
                    component.OnInitState(stateRunner);
            }
            OnInitStateComponent(_actions);
            OnInitStateComponent(_transitions);
        }

        /// <summary>
        /// Called by <see cref="StateRunner"/> when entering state.
        /// </summary>
        public void OnEnterState()
        {
            void OnEnterStateComponent(IStateLifecycle[] components)
            {
                foreach (var component in components)
                    component.OnEnterState();
            }
            OnEnterStateComponent(_actions);
            OnEnterStateComponent(_transitions);
        }

        /// <summary>
        /// Called by <see cref="StateRunner"/> when leaving state.
        /// </summary>
        public void OnExitState()
        {
            void OnExitStateComponent(IStateLifecycle[] components)
            {
                foreach (var component in components)
                    component.OnExitState();
            }
            OnExitStateComponent(_actions);
            OnExitStateComponent(_transitions);
        }

        /// <summary>
        /// Called every frame by <see cref="StateRunner"/> when updating state.
        /// </summary>
        public void OnUpdateState()
        {
            foreach (var action in _actions)
                action.Perform();
        }

        /// <summary>
        /// Checks whether the conditions to transition to the target state are met.
        /// </summary>
        /// <param name="state">
        /// Reference to a state to transition to. Null if the conditions aren't met.
        /// </param>
        /// <returns>
        /// True if the conditions are met, false - otherwise.
        /// </returns>
        internal bool OnTryGetTransition(out State state)
        {
            state = null;
            foreach (var transition in _transitions)
                if (transition.TryGetTransition(out state))
                    break;

            foreach (var transition in _transitions)
                transition.ClearStatementCache();

            return state != null;
        }
        #endregion
    }
}
