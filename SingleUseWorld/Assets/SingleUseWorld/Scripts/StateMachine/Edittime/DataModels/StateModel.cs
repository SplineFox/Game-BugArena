using System.Collections.Generic;
using UnityEngine;
using SingleUseWorld.StateMachine.Runtime;

namespace SingleUseWorld.StateMachine.Edittime
{
    /// <summary>
    /// The data container for state.
    /// </summary>
    public sealed class StateModel : ScriptableObject
    {
        #region Constants
        private const string DEFAULT_NAME = "State";
        #endregion

        #region Fields
        [SerializeField] private string _name;
        [SerializeField] private Color _color;
        [SerializeField] private List<ActionModel> _actions;
        [SerializeField] private List<TransitionModel> _transitions;

        public delegate void ValidatedDelegate();
        public event ValidatedDelegate Validated = delegate { };
        #endregion

        #region Properties
        public string Name { get => _name; internal set => _name = value; }
        public Color Color { get => _color; internal set => _color = value; }
        public IReadOnlyCollection<ActionModel> Actions { get => _actions; }
        public IReadOnlyCollection<TransitionModel> Transitions { get => _transitions; }
        #endregion

        #region Constructors
        private void Constructor()
        {
            _name = DEFAULT_NAME;
            _color = Color.gray;
            _actions = new List<ActionModel>();
            _transitions = new List<TransitionModel>();
        }

        private void Destructor()
        {
            _actions = null;
            _transitions = null;
        }
        #endregion

        #region LifeCycle Methods
        private void OnValidate()
        {
            Validated.Invoke();
        }
        #endregion

        #region Internal Methods
        internal void AddTransition(TransitionModel transition)
        {
            _transitions.Add(transition);
        }

        internal void RemoveTransition(TransitionModel transition)
        {
            _transitions.Remove(transition);
        }
        #endregion

        #region Static Methods
        public static StateModel New()
        {
            var obj = CreateInstance<StateModel>();
            obj.Constructor();
            return obj;
        }

        public static void Delete(StateModel obj)
        {
            obj.Destructor();
            DestroyImmediate(obj);
        }
        #endregion

        #region Instantiating Methods
        /// <summary>
        /// Get an existing state instance or create a new one
        /// and recursively instantiate its internal models.
        /// </summary>
        internal State GetStateInstance(Dictionary<ScriptableObject, object> createdInstances)
        {
            if (createdInstances.TryGetValue(this, out var obj))
                return (State)obj;

            var state = new State();
            state._originModel = this;
            createdInstances.Add(this, state);

            state._actions = GetActionInstances(createdInstances);
            state._transitions = GetTransitionInstances(createdInstances);

            return state;
        }

        private Action[] GetActionInstances(Dictionary<ScriptableObject, object> createdInstances)
        {
            var count = _actions.Count;
            var actions = new Action[count];
            for (int index = 0; index < count; index++)
            {
                actions[index] = _actions[index].GetActionInstance(createdInstances);
            }
            return actions;
        }

        private Transition[] GetTransitionInstances(Dictionary<ScriptableObject, object> createdInstances)
        {
            var count = _transitions.Count;
            var transitions = new Transition[count];
            for (int index = 0; index < count; index++)
            {
                transitions[index] = _transitions[index].GetTransitionInstance(createdInstances);
            }
            return transitions;
        }
        #endregion
    }
}