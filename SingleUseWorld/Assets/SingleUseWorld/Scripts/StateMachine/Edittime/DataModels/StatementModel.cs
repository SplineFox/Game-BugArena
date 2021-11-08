using System.Collections.Generic;
using UnityEngine;
using SingleUseWorld.StateMachine.Runtime;

namespace SingleUseWorld.StateMachine.Edittime
{
    /// <summary>
    /// Represents base statement model class.
    /// </summary>
    public abstract class StatementModel : ScriptableObject
    {
        #region Instantiating Methods
        /// <summary>
        /// Get an existing Statement instance or create a new one
        /// and recursively instantiate internal models.
        /// </summary>
        /// <param name="stateRunner">
        /// State machine to set as parent for new instance.
        /// </param>
        /// <param name="createdInstances">
        /// �ollection of models' instances,
        /// used to initialize object references.
        /// </param>
        /// <returns>
        /// Statement instance of this model.
        /// </returns>
        internal Statement GetStatementInstance(Dictionary<ScriptableObject, object> createdInstances)
        {
            if (createdInstances.TryGetValue(this, out var obj))
                return (Statement)obj;

            var statement = this.CreateStatementInstance();
            statement._originModel = this;
            createdInstances.Add(this, statement);
            return statement;
        }
        #endregion

        #region Protected Methods
        protected abstract Statement CreateStatementInstance();
        #endregion
    }

    /// <summary>
    /// Represents generic statement model class.
    /// </summary>
    /// <remarks>
    /// This is the base class all actual statement models must inherit from.
    /// </remarks>
    public abstract class StatementModel<T> : StatementModel where T : Statement, new()
    {
        #region Protected Methods
        protected override Statement CreateStatementInstance()
        {
            return new T();
        }
        #endregion
    }
}