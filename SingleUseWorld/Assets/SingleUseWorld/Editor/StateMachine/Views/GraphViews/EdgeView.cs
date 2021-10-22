using Edge = UnityEditor.Experimental.GraphView.Edge;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using SingleUseWorld.StateMachine.Models;
using UnityEngine.UIElements;

namespace SingleUseWorld.StateMachine.Views
{
    public sealed class EdgeView : Edge
    {
        #region Fields
        private GraphView _graph;
        private EdgeModel _model;
        #endregion

        #region Properties
        public EdgeModel Model { get => _model; }
        #endregion

        #region Public Methods
        public void LoadEdgeModel(GraphView graph, EdgeModel model)
        {
            _graph = graph;
            _model = model;
            viewDataKey = _model.Guid;
        }
        #endregion
    }
}