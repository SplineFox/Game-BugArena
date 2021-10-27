using System;
using System.Collections.Generic;
using UnityEngine;

namespace SingleUseWorld.StateMachine.Models
{
    [Serializable]
    public struct ConditionModel
    {
        #region Nested Classes
        public enum ResultModel { True, False }
        #endregion

        #region Fields
        public StatementModel Statement;
        public ResultModel ExpectedResult;
        #endregion

        #region Internal Methods
        internal Condition GetConditionInstance(StateMachine stateMachine, Dictionary<ScriptableObject, object> createdInstances)
        {
            var statement = Statement.GetStatementInstance(stateMachine, createdInstances);
            var expectedResult = ExpectedResult == ResultModel.True;
            var condition = new Condition(statement, expectedResult);

            return condition;
        }
        #endregion
    }
}