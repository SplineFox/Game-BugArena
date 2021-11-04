using System.Collections.Generic;
using UnityEngine;
using SingleUseWorld.StateMachine.Runtime;

namespace SingleUseWorld.StateMachine.Edittime
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

        #region Instantiating Methods
        internal Transition GetTransitionInstance(Dictionary<ScriptableObject, object> createdInstances)
        {
            if (createdInstances.TryGetValue(this, out var obj))
                return (Transition)obj;

            var target = _target.GetStateInstance(createdInstances);
            var conditions = GetConditionInstances(createdInstances);

            var transition = new Transition(target, conditions);
            createdInstances.Add(this, transition);
            return transition;
        }

        private Condition[] GetConditionInstances(Dictionary<ScriptableObject, object> createdInstances)
        {
            var count = _conditions.Count;
            var conditions = new Condition[count];
            for (int index = 0; index < count; index++)
            {
                conditions[index] = _conditions[index].GetConditionInstance(createdInstances);
            }
            return conditions;
        }
        #endregion
    }
}