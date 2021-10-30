using System;
using UnityEngine;
using Edge = UnityEditor.Experimental.GraphView.Edge;

namespace SingleUseWorld.StateMachine.EditorTime
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
            if (_edgeModel.Source is InitialNodeModel)
                return;

            base.OnSelected();
            OnEdgeSelected?.Invoke(_edgeModel.Transition);
        }
        #endregion
    }
}