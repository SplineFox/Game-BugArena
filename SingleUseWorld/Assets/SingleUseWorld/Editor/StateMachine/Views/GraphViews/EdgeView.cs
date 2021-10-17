using UnityEngine;
using UnityEditor.Experimental.GraphView;
using GraphEdgeView = UnityEditor.Experimental.GraphView.Edge;
using SingleUseWorld.StateMachine.Models;

namespace SingleUseWorld.StateMachine.Views
{
    public sealed class EdgeView : GraphEdgeView
    {
        #region Fields
        GraphEdgeModel _model;
        #endregion

        #region Properties
        public GraphEdgeModel Model { get => _model; }
        #endregion

        #region Public Methods
        public void SetModel(GraphEdgeModel model)
        {
            _model = model;
            viewDataKey = _model.Guid.ToString();
        }
        #endregion
    }
}