using System.Collections.Generic;
using UnityEngine;

namespace SingleUseWorld.StateMachine.Models
{
    /// <summary>
    /// Data container for state-node element.
    /// </summary>
    public abstract class GraphNodeModel : GraphElementModel
    {
        #region Fields
        protected Vector2 _position;
        protected StateModel _state;
        protected List<GraphEdgeModel> _inputs;
        protected List<GraphEdgeModel> _outputs;
        #endregion

        #region Properties
        public Vector2 Position { get => _position; set => _position = value; }
        public StateModel State { get => _state; }
        public IReadOnlyList<GraphEdgeModel> Inputs { get => _inputs; }
        public IReadOnlyList<GraphEdgeModel> Outputs { get => _outputs; }
        #endregion

        #region Constructors & Destructors
        protected void Constructor(GraphModel graph, Vector2 position, StateModel state)
        {
            base.Constructor(graph);
            _position = position;
            _state = state;
            _inputs = new List<GraphEdgeModel>();
            _outputs = new List<GraphEdgeModel>();
        }

        protected void Destructor()
        {
            base.Destructor();
            _state = null;
            _inputs = null;
            _outputs = null;
        }
        #endregion

        #region Public Methods
        internal void AddInput(GraphEdgeModel input)
        {
            _inputs.Add(input);
        }

        internal void AddOutput(GraphEdgeModel output)
        {
            _outputs.Add(output);
        }

        internal void RemoveInput(GraphEdgeModel input)
        {
            _inputs.Remove(input);
        }

        internal void RemoveOutput(GraphEdgeModel output)
        {
            _outputs.Remove(output);
        }
        #endregion
    }
}