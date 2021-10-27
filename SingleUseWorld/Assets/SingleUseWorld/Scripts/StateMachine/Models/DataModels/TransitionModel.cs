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
        [SerializeField] private StateModel _source;
        [SerializeField] private StateModel _target;
        [SerializeField] private List<ConditionModel> _conditions;
        #endregion

        #region Properties
        public string DisplayName { get => _source.Name + "->" + _target.Name; }
        public StateModel Source { get => _source; }
        public StateModel Target { get => _target; }
        public IReadOnlyCollection<ConditionModel> Conditions { get => _conditions; }
        #endregion

        #region Constructors
        private void Constructor(StateModel source, StateModel target)
        {
            _source = source;
            _target = target;
            _conditions = new List<ConditionModel>();
        }

        private void Destructor()
        {
            _source = null;
            _target = null;
            _conditions = null;
        }
        #endregion

        #region Internal Methods
        internal Transition GetTransitionInstance(StateMachine stateMachine, Dictionary<ScriptableObject, object> createdInstances)
        {
            if (createdInstances.TryGetValue(this, out var obj))
                return (Transition)obj;

            var target = _target.GetStateInstance(stateMachine, createdInstances);
            var conditions = GetConditionInstances(stateMachine, createdInstances);

            var transition = new Transition(target, conditions);
            createdInstances.Add(this, transition);
            return transition;
        }

        private Condition[] GetConditionInstances(StateMachine stateMachine, Dictionary<ScriptableObject, object> createdInstances)
        {
            var count = _conditions.Count;
            var conditions = new Condition[count];
            for (int index = 0; index < count; index++)
            {
                conditions[index] = _conditions[index].GetConditionInstance(stateMachine, createdInstances);
            }
            return conditions;
        }
        #endregion

        #region Static Methods
        public static TransitionModel New(StateModel source, StateModel target)
        {
            var obj = CreateInstance<TransitionModel>();
            obj.Constructor(source, target);
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