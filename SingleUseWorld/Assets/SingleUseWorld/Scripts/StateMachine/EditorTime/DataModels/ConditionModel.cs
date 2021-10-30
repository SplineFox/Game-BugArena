using System;
using System.Collections.Generic;
using UnityEngine;
using SingleUseWorld.StateMachine.RunTime;

namespace SingleUseWorld.StateMachine.EditorTime
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

        #region Instantiating Methods
        internal Condition GetConditionInstance(StateRunner stateRunner, Dictionary<ScriptableObject, object> createdInstances)
        {
            var statement = Statement.GetStatementInstance(stateRunner, createdInstances);
            var expectedResult = ExpectedResult == ResultModel.True;
            var condition = new Condition(statement, expectedResult);

            return condition;
        }
        #endregion
    }
}