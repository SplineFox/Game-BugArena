using SingleUseWorld.StateMachine.EditorTime;

namespace SingleUseWorld.StateMachine.RunTime
{
    /// <summary>
    /// Represents an action to be performed in a state.
    /// </summary>
    /// <remarks>
    /// This is the base class all custom actions must inherit from.
    /// </remarks>
    public abstract class Action : IStateLifecycle
    {
        #region Fields
        internal ActionModel _originModel;
        #endregion

        #region Properties
        protected ActionModel OriginModel { get => _originModel; }
        #endregion

        #region Public Methods
        /// <inheritdoc/>
        public virtual void OnInitState(StateRunner stateRunner) { }

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
