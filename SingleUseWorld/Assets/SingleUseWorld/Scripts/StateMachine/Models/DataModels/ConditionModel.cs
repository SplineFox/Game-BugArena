using System;
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
        public StatementModel statement;
        public ResultModel expectedResult;
        #endregion
    }
}