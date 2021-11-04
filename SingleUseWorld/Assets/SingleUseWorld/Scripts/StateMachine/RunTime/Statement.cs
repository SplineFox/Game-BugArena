using SingleUseWorld.StateMachine.Edittime;

namespace SingleUseWorld.StateMachine.Runtime
{
    /// <summary>
    /// Represents a statement to be evaluated in a transition condition.
    /// </summary>
    /// <remarks>
    /// This is the base class all custom statement must inherit from.
    /// </remarks>
    public abstract class Statement : IStateLifecycle
    {
        #region Fields
        internal StatementModel _originModel;

        private bool _evaluationIsCached = false;
        private bool _cachedEvaluation = default;
        #endregion

        #region Public Methods
        /// <inheritdoc/>
        public virtual void OnInitState(StateRunner stateRunner) { }

        /// <inheritdoc/>
        public virtual void OnEnterState() { }

        /// <inheritdoc/>
        public virtual void OnExitState() { }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Wraps the <see cref="Evaluate"/> so it can be cached
        /// to avoid re-calculations within a single update.
        /// </summary>
        internal bool GetEvaluation()
        {
            if (!_evaluationIsCached)
            {
                _cachedEvaluation = Evaluate();
                _evaluationIsCached = true;
            }
            return _cachedEvaluation;
        }

        /// <summary>
        /// Clears the cached evaluation so that it can be recalculated on next update.
        /// </summary>
        internal void ClearEvaluationCache()
        {
            _evaluationIsCached = false;
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Specifies the statement to evaluate.
        /// </summary>
        protected abstract bool Evaluate();
        #endregion
    }
}
