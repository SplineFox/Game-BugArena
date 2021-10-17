using System;
using System.Collections.Generic;
using UnityEngine;

namespace SingleUseWorld.StateMachine.Models
{
    /// <summary>
    /// The data container for transition.
    /// </summary>
    public sealed class TransitionModel : ScriptableObject
    {
        #region Fields
        [SerializeField] private StateModel _target;
        [SerializeField] private List<ConditionModel> _conditions;
        #endregion

        #region Properties
        public StateModel Target { get => _target; }
        public IReadOnlyCollection<ConditionModel> Conditions { get => _conditions; }
        #endregion

        #region Constructors
        private void Constructor(StateModel target)
        {
            _target = target;
            _conditions = new List<ConditionModel>();
        }

        private void Destructor()
        {
            _target = null;
            _conditions = null;
        }
        #endregion

        #region Static Methods
        public static TransitionModel New(StateModel target)
        {
            var obj = CreateInstance<TransitionModel>();
            obj.Constructor(target);
            return obj;
        }

        public static void Delete(TransitionModel obj)
        {
            obj.Destructor();
            DestroyImmediate(obj);
        }
        #endregion
    }
}