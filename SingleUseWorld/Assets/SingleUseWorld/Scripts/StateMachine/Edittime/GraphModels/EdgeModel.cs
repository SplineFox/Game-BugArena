using System;
using UnityEngine;

namespace SingleUseWorld.StateMachine.Edittime
{
    [Serializable]
    public sealed class EdgeModel : GraphElementModel
    {
        #region Fields
        [SerializeField] private TransitionModel _transition;

        [SerializeReference] private NodeModel _source;
        [SerializeReference] private NodeModel _target;
        #endregion

        #region Properties
        public TransitionModel Transition { get => _transition;}
        public NodeModel Source { get => _source; }
        public NodeModel Target { get => _target; }
        #endregion

        #region Constructors
        public EdgeModel(TransitionModel transition, NodeModel source, NodeModel target)
        {
            _transition = transition;
            _source = source;
            _target = target;
        }
        #endregion

        #region Internal Methods
        internal override void OnAfterAddedToGraph()
        {
            _source.AddOutput(this);
            _source.State.AddTransition(_transition);
            _target.AddInput(this);
        }

        internal override void OnBeforeRemovedFromGraph()
        {
            _target.RemoveInput(this);
            _source.State.RemoveTransition(_transition);
            _source.RemoveOutput(this);
        }
        #endregion
    }
}