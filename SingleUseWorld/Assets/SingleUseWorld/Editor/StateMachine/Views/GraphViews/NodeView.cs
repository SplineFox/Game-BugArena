using Node = UnityEditor.Experimental.GraphView.Node;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using SingleUseWorld.StateMachine.Models;
using UnityEngine.UIElements;
using System;

namespace SingleUseWorld.StateMachine.Views
{
    public sealed class NodeView : Node
    {
        #region Fields
        private GraphView _graph;
        private NodeModel _model;
        private Port _input;
        private Port _output;
        #endregion

        #region Properties
        public NodeModel Model { get => _model; }
        public Port Input { get => _input; }
        public Port Output { get => _output; }
        #endregion

        #region Public Methods
        public void SetModel(GraphView graph, NodeModel model)
        {
            _graph = graph;
            _model = model;
            viewDataKey = _model.Guid;

            SetPosition(new Rect(_model.Position, Vector2.one));

            CreatePort();

            OnValidateView();
            RegisterCallback<AttachToPanelEvent>(OnEnableView);
            RegisterCallback<DetachFromPanelEvent>(OnDisableView);
        }

        public override void SetPosition(Rect newPosition)
        {
            base.SetPosition(newPosition);
            _model.Position = newPosition.position;
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            if (_model is MasterNodeModel)
            {
                evt.menu.AppendAction($"Create slave state", (action) => RequestToCreateSlaveNode());
            }

            if (_model is SlaveNodeModel)
            {
                evt.menu.AppendAction($"Go to master state", (action) => RequestToGoToMasterNode());
            }

            evt.menu.AppendSeparator();
        }
        #endregion

        #region Private Methods
        private void OnEnableView(AttachToPanelEvent evt)
        {
            Model.State.Validated += OnValidateView;
        }

        private void OnDisableView(DetachFromPanelEvent evt)
        {
            Model.State.Validated -= OnValidateView;
        }

        private void OnValidateView()
        {
            title = Model.State.name;
            titleContainer.style.backgroundColor = Model.State.Color;
        }

        private void CreatePort()
        {
            if (_model is MasterNodeModel)
                CreateOutputPort();

            if (_model is SlaveNodeModel)
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

        private void RequestToCreateSlaveNode()
        {
            _graph.RequestToCreateSlaveNode(this);
        }

        private void RequestToGoToMasterNode()
        {
            var slave = _model as SlaveNodeModel;
            _graph.MoveViewpointTo(slave.Master.Position);
        }
        #endregion
    }
}