using System;
using System.Collections.Generic;
using UnityEngine;

namespace SingleUseWorld.StateMachine.EditorTime
{
    [Serializable]
    public abstract class NodeModel : GraphElementModel
    {
        #region Fields
        [SerializeField] private StateModel _state;
        [SerializeField] private Vector2 _position;

        [SerializeReference] private List<EdgeModel> _inputs;
        [SerializeReference] private List<EdgeModel> _outputs;
        #endregion

        #region Properties
        public StateModel State { get => _state; }
        public Vector2 Position { get => _position; set => _position = value; }
        public IReadOnlyList<EdgeModel> Inputs { get => _inputs; }
        public IReadOnlyList<EdgeModel> Outputs { get => _outputs; }
        #endregion

        #region Constructors
        public NodeModel(StateModel state, Vector2 position)
        {
            _state = state;
            _position = position;
            _inputs = new List<EdgeModel>();
            _outputs = new List<EdgeModel>();
        }
        #endregion

        #region Public Methods
        public bool HasOutputWithState(StateModel state)
        {
            return _outputs.Find((EdgeModel edge) => edge.Target.State == state) != null;
        }

        public bool HasInputWithState(StateModel state)
        {
            return _inputs.Find((EdgeModel edge) => edge.Source.State == state) != null;
        }
        #endregion

        #region Internal Methods
        internal void AddInput(EdgeModel input)
        {
            _inputs.Add(input);
        }

        internal void AddOutput(EdgeModel output)
        {
            _outputs.Add(output);
        }

        internal void RemoveInput(EdgeModel input)
        {
            _inputs.Remove(input);
        }

        internal void RemoveOutput(EdgeModel output)
        {
            _outputs.Remove(output);
        }
        #endregion
    }
}