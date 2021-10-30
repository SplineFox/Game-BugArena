using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using Node = UnityEditor.Experimental.GraphView.Node;

namespace SingleUseWorld.StateMachine.EditorTime
{
    public sealed class NodeView : Node
    {
        #region Fields
        public Action<ScriptableObject> OnNodeSelected;

        private GraphView _graph;
        private NodeModel _nodeModel;
        private Port _input;
        private Port _output;
        #endregion

        #region Properties
        public NodeModel Model { get => _nodeModel; }
        public Port Input { get => _input; }
        public Port Output { get => _output; }
        #endregion

        #region Public Methods
        public void LoadNodeModel(GraphView graph, NodeModel nodeModel)
        {
            _graph = graph;
            _nodeModel = nodeModel;
            viewDataKey = _nodeModel.Guid;

            SetPosition(new Rect(_nodeModel.Position, Vector2.one));

            CreatePort();

            OnValidateView();
            RegisterCallback<AttachToPanelEvent>(OnEnableView);
            RegisterCallback<DetachFromPanelEvent>(OnDisableView);
        }

        public override void SetPosition(Rect newPosition)
        {
            base.SetPosition(newPosition);
            _nodeModel.Position = newPosition.position;
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            if (_nodeModel is MasterNodeModel)
            {
                evt.menu.AppendAction($"Create slave state", (action) => RequestToCreateSlaveNode());
            }

            if (_nodeModel is SlaveNodeModel)
            {
                evt.menu.AppendAction($"Go to master state", (action) => RequestToGoToMasterNode());
            }

            evt.menu.AppendSeparator();
        }

        public override void OnSelected()
        {
            if (_nodeModel is InitialNodeModel)
                return;

            base.OnSelected();
            OnNodeSelected?.Invoke(_nodeModel.State);
        }
        #endregion

        #region Private Methods
        private void OnEnableView(AttachToPanelEvent evt)
        {
            SubscribeToNodeModel();
        }

        private void OnDisableView(DetachFromPanelEvent evt)
        {
            UnsubscribeFromNodeModel();
        }

        private void OnValidateView()
        {
            title = _nodeModel.State.Name;
            titleContainer.style.backgroundColor = _nodeModel.State.Color;
        }

        private void SubscribeToNodeModel()
        {
            if (_nodeModel != null)
            {
                _nodeModel.State.Validated += OnValidateView;
            }
        }

        private void UnsubscribeFromNodeModel()
        {
            if (_nodeModel != null)
            {
                _nodeModel.State.Validated -= OnValidateView;
            }
        }

        private void CreatePort()
        {
            if (_nodeModel is InitialNodeModel)
            {
                CreateOutputPort(Port.Capacity.Single, "Initial");
                Output.portColor = _nodeModel.State.Color;
            }

            if (_nodeModel is MasterNodeModel)
            {
                CreateOutputPort(Port.Capacity.Multi, "Source");
            }

            if (_nodeModel is SlaveNodeModel)
            {
                CreateInputPort(Port.Capacity.Single, "Target");
            }

            RefreshExpandedState();
            RefreshPorts();
        }

        private void CreateOutputPort(Port.Capacity capacity, string name)
        {
            Port outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, capacity, typeof(bool));
            if (outputPort != null)
            {
                outputPort.portName = name;
                outputContainer.Add(outputPort);
                _output = outputPort;
            }
        }
        private void CreateInputPort(Port.Capacity capacity, string name)
        {
            Port inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, capacity, typeof(bool));
            if (inputPort != null)
            {
                inputPort.portName = name;
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
            var slave = _nodeModel as SlaveNodeModel;
            _graph.MoveViewpointTo(slave.Master.Position);
        }
        #endregion
    }
}