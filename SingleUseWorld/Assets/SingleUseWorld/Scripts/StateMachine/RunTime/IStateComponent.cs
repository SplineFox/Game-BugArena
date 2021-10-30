namespace SingleUseWorld.StateMachine.RunTime
{
    /// <summary>
    /// Represents an interface of a component participating
    /// in a state lifecycle it belongs to.
    /// </summary>
    public interface IStateComponent
    {
        #region Public Methods
        /// <summary>
        /// Called by <see cref="State"/> when initializing state.
        /// </summary>
        public void OnInitState();

        /// <summary>
        /// Called by <see cref="State"/> when entering state.
        /// </summary>
        public void OnEnterState();

        /// <summary>
        /// Called by <see cref="State"/> when leaving state.
        /// </summary>
        public void OnExitState();
        #endregion
    }
}
