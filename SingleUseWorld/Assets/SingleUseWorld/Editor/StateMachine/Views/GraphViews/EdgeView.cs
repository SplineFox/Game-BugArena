using Edge = UnityEditor.Experimental.GraphView.Edge;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using SingleUseWorld.StateMachine.Models;
using UnityEngine.UIElements;
using System;

namespace SingleUseWorld.StateMachine.Views
{
    public sealed class EdgeView : Edge
    {
        #region Fields
        public Action<ScriptableObject> OnEdgeSelected;

        private GraphView _graph;
        private EdgeModel _edgeModel;
        #endregion

        #region Properties
        public EdgeModel Model { get => _edgeModel; }
        #endregion

        #region Public Methods
        public void LoadEdgeModel(GraphView graph, EdgeModel model)
        {
            _graph = graph;
            _edgeModel = model;
            viewDataKey = _edgeModel.Guid;
        }

        public override void OnSelected()
        {
            base.OnSelected();
            OnEdgeSelected?.Invoke(_edgeModel.Transition);
        }
        #endregion
    }
}