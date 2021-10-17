using UnityEngine;
using UnityEditor.Experimental.GraphView;
using GraphNodeView = UnityEditor.Experimental.GraphView.Node;
using SingleUseWorld.StateMachine.Models;

namespace SingleUseWorld.StateMachine.Views
{
    public sealed class NodeView : GraphNodeView
    {
        #region Fields
        private GraphNodeModel _model;
        private Port _input;
        private Port _output;
        #endregion

        #region Properties
        public GraphNodeModel Model { get => _model; }
        public Port Input { get => _input; }
        public Port Output { get => _output; }
        #endregion

        #region Public Methods
        public void SetModel(GraphNodeModel model)
        {
            _model = model;
            viewDataKey = _model.Guid.ToString();

            style.left = _model.Position.x;
            style.top = _model.Position.y;

            UpdateView();
            CreatePort();
        }

        public override void SetPosition(Rect newPosition)
        {
            base.SetPosition(newPosition);
            _model.Position = new Vector2(newPosition.xMin, newPosition.yMin);
        }
        #endregion

        #region Private Methods
        private void UpdateView()
        {
            title = _model.name;
        }

        private void CreatePort()
        {
            if (_model is GraphMasterNodeModel)
                CreateOutputPort();

            if (_model is GraphSlaveNodeModel)
                CreateInputPort();
        }

        private void CreateOutputPort()
        {
            Port outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
            if (outputPort != null)
            {
                outputPort.portName = "Source";
                outputContainer.Add(outputPort);
                _output = outputPort;
            }
        }

        private void CreateInputPort()
        {
            Port inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
            if (inputPort != null)
            {
                inputPort.portName = "Target";
                inputContainer.Add(inputPort);
                _input = inputPort;
            }
        }
        #endregion
    }
}