using System.Collections.Generic;
using UnityEngine;

namespace SingleUseWorld.StateMachine.Models
{
    /// <summary>
    /// The data container for state.
    /// </summary>
    public sealed class StateModel : ScriptableObject
    {
        #region Fields
        [SerializeField] private Color _color;
        [SerializeField] private List<ActionModel> _actions;
        [SerializeField] private List<TransitionModel> _transitions;
        #endregion

        #region Properties
        public IReadOnlyCollection<ActionModel> Actions { get => _actions; }
        public IReadOnlyCollection<TransitionModel> Transitions { get => _transitions; }
        #endregion

        #region Constructors
        private void Constructor()
        {
            _actions = new List<ActionModel>();
            _transitions = new List<TransitionModel>();
        }

        private void Destructor()
        {
            _actions = null;
            _transitions = null;
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
    }
}