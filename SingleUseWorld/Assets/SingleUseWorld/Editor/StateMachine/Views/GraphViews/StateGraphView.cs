using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using SingleUseWorld.StateMachine.Models;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using System;

namespace SingleUseWorld.StateMachine.Views
{
    public class StateGraphView : GraphView
    {
        #region Nested Classes
        public new class UxmlFactory : UxmlFactory<StateGraphView, GraphView.UxmlTraits> { }
        #endregion

        #region Fields
        private GraphModel _model;
        private Vector2 _lastMousePosition;
        #endregion

        #region Properties
        public GraphModel Model { get => _model; }
        #endregion

        #region Constructors
        public StateGraphView()
        {
            InitUss();
            InitElements();
            InitManipulators();
            InitCallbacks();
        }
        #endregion

        #region Public Methods
        public void SetModel(GraphModel model)
        {
            if (_model != null)
            {
                UnsubscribeFromModel(_model);
                DepopulateGraphView();
            }
            _model = model;
            SubscribeToModel(_model);
            PopulateGraphView();
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList().Where(endPort =>
                endPort.direction != startPort.direction &&
                endPort.node != startPort.node).ToList();
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            evt.menu.AppendAction($"Create master state", (action) => RequestToCreateMasterNode());
        }
        #endregion

        #region Private Methods
        private void InitUss()
        {
            var uss = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/SingleUseWorld/Editor/StateMachine/Views/USS/StateGraphEditorWindow.uss");
            this.styleSheets.Add(uss);
        }

        private void InitElements()
        {
            this.Insert(0, new GridBackground());
        }

        private void InitManipulators()
        {
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
        }

        private void InitCallbacks()
        {
            this.RegisterCallback<MouseMoveEvent>(OnGraphMouseMove);
        }

        private void SubscribeToModel(GraphModel model)
        {
            model.NodeCreated += OnNodeCreated;
            model.EdgeCreated += OnEdgeCreated;
            model.NodeAboutToBeDestroyed += OnNodeDestroyed;
            model.EdgeAboutToBeDestroyed += OnEdgeDestroyed;
            this.graphViewChanged += OnGraphViewChanged;
        }

        private void UnsubscribeFromModel(GraphModel model)
        {
            model.NodeCreated -= OnNodeCreated;
            model.EdgeCreated -= OnEdgeCreated;
            model.NodeAboutToBeDestroyed -= OnNodeDestroyed;
            model.EdgeAboutToBeDestroyed -= OnEdgeDestroyed;
            this.graphViewChanged -= OnGraphViewChanged;
        }

        private void PopulateGraphView()
        {
            foreach (var node in _model.Nodes)
                CreateNodeView(node);

            foreach (var edge in _model.Edges)
                CreateEdgeView(edge);
        }

        private void DepopulateGraphView()
        {
            this.graphViewChanged -= OnGraphViewChanged;
            this.DeleteElements(graphElements.ToList());
            this.graphViewChanged += OnGraphViewChanged;
        }

        private void CreateNodeView(GraphNodeModel node)
        {
            var view = new NodeView();
            view.SetModel(node);
            
            this.graphViewChanged -= OnGraphViewChanged;
            this.AddElement(view);
            this.graphViewChanged += OnGraphViewChanged;
        }

        private void CreateEdgeView(GraphEdgeModel edge)
        {
            var view = ConnectEdge(edge);
            view.SetModel(edge);

            this.graphViewChanged -= OnGraphViewChanged;
            this.AddElement(view);
            this.graphViewChanged += OnGraphViewChanged;
        }

        private void DestroyNodeView(GraphNodeModel node)
        {
            var view = this.GetNodeByGuid(node.Guid.ToString());

            this.graphViewChanged -= OnGraphViewChanged;
            this.RemoveElement(view);
            this.graphViewChanged += OnGraphViewChanged;
        }

        private void DestroyEdgeView(GraphEdgeModel edge)
        {
            var view = this.GetEdgeByGuid(edge.Guid.ToString());
            DisconnectEdge(view);

            this.graphViewChanged -= OnGraphViewChanged;
            this.RemoveElement(view);
            this.graphViewChanged += OnGraphViewChanged;
        }

        private EdgeView ConnectEdge(GraphEdgeModel edge)
        {
            var sourceView = this.GetNodeByGuid(edge.Source.Guid.ToString()) as NodeView;
            var targetView = this.GetNodeByGuid(edge.Target.Guid.ToString()) as NodeView;
            var edgeView = sourceView.Input.ConnectTo<EdgeView>(targetView.Output);

            return edgeView;
        }

        private void DisconnectEdge(Edge graphEdge)
        {
            graphEdge.input.Disconnect(graphEdge);
            graphEdge.output.Disconnect(graphEdge);
            graphEdge.input = null;
            graphEdge.output = null;
        }

        private void OnGraphMouseMove(MouseMoveEvent evt)
        {
            _lastMousePosition = evt.mousePosition;
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange change)
        {
            if (change.edgesToCreate != null)
            {
                foreach (var edge in change.edgesToCreate)
                {
                    RequestToCreateEdge(edge);
                }
                change.edgesToCreate.Clear();
            }

            if (change.elementsToRemove != null)
            {
                foreach (var element in change.elementsToRemove)
                {
                    if (element is NodeView nodeView)
                    {
                        RequestToDestroyNode(nodeView);
                    }
                    else if (element is EdgeView edgeView)
                    {
                        RequestToDestroyEdge(edgeView);
                    }
                }
                change.elementsToRemove.Clear();
            }

            // We clear change lists because we perform
            // element add/remove - operations ourselves
            // and don't want the graph to handle this,
            // exept of "GraphViewChange.movedElements".
            return change;
        }

        private void OnNodeCreated(GraphNodeModel node)
        {
            CreateNodeView(node);
        }

        private void OnEdgeCreated(GraphEdgeModel edge)
        {
            CreateEdgeView(edge);
        }

        private void OnNodeDestroyed(GraphNodeModel node)
        {
            DestroyNodeView(node);
        }

        private void OnEdgeDestroyed(GraphEdgeModel edge)
        {
            DestroyEdgeView(edge);
        }

        private void RequestToCreateMasterNode()
        {
            Vector2 position = contentViewContainer.WorldToLocal(_lastMousePosition);
            _model.CreateMasterNode(position);
        }

        private void RequestToCreateSlaveNode(NodeView nodeView)
        {
            Rect nodeViewRect = nodeView.GetPosition();
            Vector2 position = new Vector2(nodeViewRect.xMin + 20, nodeViewRect.yMin + 20);
            _model.CreateSlaveNode(position, nodeView.Model);
        }

        private void RequestToCreateEdge(Edge edge)
        {
            var sourceView = edge.input.node as NodeView;
            var targetView = edge.output.node as NodeView;
            _model.CreateEdge(sourceView.Model, targetView.Model);
        }

        private void RequestToDestroyNode(NodeView nodeView)
        {
            _model.DestroyNode(nodeView.Model);
        }

        private void RequestToDestroyEdge(EdgeView edgeView)
        {
            _model.DestroyEdge(edgeView.Model);
        }
        #endregion
    }
}