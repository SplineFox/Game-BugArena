using SingleUseWorld.StateMachine.Models;

namespace SingleUseWorld.StateMachine
{
    /// <summary>
    /// Represents a statement to be evaluated in a transition condition.
    /// </summary>
    /// <remarks>
    /// This is the base class all custom statement must inherit from.
    /// </remarks>
    public abstract class Statement
    {
        #region Fields
        internal StateMachine _stateMachine;

        private bool _evaluationIsCached = false;
        private bool _cachedEvaluation = default;
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
