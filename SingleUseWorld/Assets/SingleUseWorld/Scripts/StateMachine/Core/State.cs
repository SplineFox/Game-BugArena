using SingleUseWorld.StateMachine.Models;

namespace SingleUseWorld.StateMachine
{
    /// <summary>
    /// Represents the implementation of the State pattern.
    /// </summary>
    public class State
    {
        #region Fields
        internal StateMachine _stateMachine;

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
        /// Called by <see cref="StateMachine"/> when initializing state.
        /// </summary>
        /// <param name="stateMachine">
        /// State machine this instance belongs to.
        /// </param>
        internal void OnInitState()
        {
            void OnInitStateComponent(IStateComponent[] components)
            {
                foreach (var component in components)
                    component.OnInitState();
            }
            OnInitStateComponent(_actions);
            OnInitStateComponent(_transitions);
        }

        /// <summary>
        /// Called by <see cref="StateMachine"/> when entering state.
        /// </summary>
        internal void OnEnterState()
        {
            void OnEnterStateComponent(IStateComponent[] components)
            {
                foreach (var component in components)
                    component.OnEnterState();
            }
            OnEnterStateComponent(_actions);
            OnEnterStateComponent(_transitions);
        }

        /// <summary>
        /// Called by <see cref="StateMachine"/> when leaving state.
        /// </summary>
        internal void OnExitState()
        {
            void OnExitStateComponent(IStateComponent[] components)
            {
                foreach (var component in components)
                    component.OnExitState();
            }
            OnExitStateComponent(_actions);
            OnExitStateComponent(_transitions);
        }

        /// <summary>
        /// Called every frame by <see cref="StateMachine"/> when updating state.
        /// </summary>
        internal void OnUpdateState()
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
