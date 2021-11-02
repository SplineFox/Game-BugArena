namespace SingleUseWorld.StateMachine.RunTime
{
    /// <summary>
    /// Represents an interface of a component participating
    /// in a state lifecycle it belongs to.
    /// </summary>
    public interface IStateLifecycle
    {
        #region Public Methods
        /// <summary>
        /// Called when initializing state.
        /// </summary>
        public void OnInitState(StateRunner stateRunner);

        /// <summary>
        /// Called when entering state.
        /// </summary>
        public void OnEnterState();

        /// <summary>
        /// Called  when leaving state.
        /// </summary>
        public void OnExitState();
        #endregion
    }
}
