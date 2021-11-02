using System.Collections.Generic;
using UnityEngine;
using SingleUseWorld.StateMachine.RunTime;

namespace SingleUseWorld.StateMachine.EditorTime
{
    /// <summary>
    /// Represents base action model class.
    /// </summary>
    public abstract class ActionModel : ScriptableObject
    {
        #region Instantiating Methods
        /// <summary>
        /// Get an existing action instance or create a new one
        /// and recursively instantiate internal models.
        /// </summary>
        /// <param name="stateRunner">
        /// State machine to set as parent for new instance.
        /// </param>
        /// <param name="createdInstances">
        /// Ñollection of models' instances,
        /// used to initialize object references.
        /// </param>
        /// <returns>
        /// Action instance of this model.
        /// </returns>
        internal Action GetActionInstance(Dictionary<ScriptableObject, object> createdInstances)
        {
            if (createdInstances.TryGetValue(this, out var obj))
                return (Action)obj;

            var action = this.CreateActionInstance();
            action._originModel = this;
            createdInstances.Add(this, action);
            return action;
        }
        #endregion

        #region Protected Methods
        protected abstract Action CreateActionInstance();
        #endregion
    }

    /// <summary>
    /// Represents generic action model.
    /// </summary>
    /// <remarks>
    /// This is the base class all actual action models must inherit from.
    /// </remarks>
    public abstract class ActionModel<T> : ActionModel where T : Action, new()
    {
        #region Protected Methods
        protected override Action CreateActionInstance()
        {
            return new T();
        }
        #endregion
    }
}