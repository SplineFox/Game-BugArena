namespace SingleUseWorld.StateMachine.Runtime
{
    /// <summary>
    /// Represents a transition to a target state
    /// to be performed when conditions are met.
    /// </summary>
    public class Transition : IStateLifecycle
    {
        #region Fields
        private State _target;
        private Condition[] _conditions;
        #endregion

        #region Constructors
        public Transition(State target, Condition[] conditions)
        {
            _target = target;
            _conditions = conditions;
        }
        #endregion

        #region Public Methods
        /// <inheritdoc/>
        public void OnInitState(StateRunner stateRunner)
        {
            foreach (var condition in _conditions)
                condition._statement.OnInitState(stateRunner);
        }

        /// <inheritdoc/>
        public void OnEnterState() 
        {
            foreach (var condition in _conditions)
                condition._statement.OnEnterState();
        }

        /// <inheritdoc/>
        public void OnExitState()
        {
            foreach (var condition in _conditions)
                condition._statement.OnExitState();
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Checks whether the conditions to transition to the target state are met.
        /// </summary>
        /// <param name="state">
        /// Reference to a state to transition to. Null if the conditions aren't met.
        /// </param>
        /// <returns>
        /// True if the conditions are met, false - otherwise.
        /// </returns>
        internal bool TryGetTransition(out State state)
        {
            state = ShouldTransition() ? _target : null;
            return state != null;
        }

        /// <summary>
        /// Clears statement evaluations of the conditions
        /// so that they can be recalculated on next update.
        /// </summary>
        internal void ClearStatementCache()
        {
            foreach(var condition in _conditions)
                condition._statement.ClearEvaluationCache();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Checks whether all of conditions are met.
        /// </summary>
        /// <returns>
        /// True if all of conditions are met, false - otherwise.
        /// </returns>
        private bool ShouldTransition()
        {
            bool result = true;
            foreach(var condition in _conditions)
                result = result && condition.IsMet();

            return result;
        }
        #endregion
    }
}
