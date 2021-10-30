namespace SingleUseWorld.StateMachine.RunTime
{
    /// <summary>
    /// Represents an action to be performed in a state.
    /// </summary>
    /// <remarks>
    /// This is the base class all custom actions must inherit from.
    /// </remarks>
    public abstract class Action : IStateComponent
    {
        #region Fields
        internal StateRunner _stateRunner;
        #endregion

        #region Public Methods
        /// <inheritdoc/>
        public virtual void OnInitState() { }

        /// <inheritdoc/>
        public virtual void OnEnterState() { }

        /// <inheritdoc/>
        public virtual void OnExitState() { }

        /// <summary>
        /// Specifies the action to perform.
        /// </summary>
        public abstract void Perform();
        #endregion
    }
}
