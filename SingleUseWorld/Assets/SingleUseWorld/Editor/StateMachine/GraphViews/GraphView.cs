using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using Graph = UnityEditor.Experimental.GraphView.GraphView;

namespace SingleUseWorld.StateMachine.EditorTime
{
    public sealed class GraphView : Graph
    {
        #region Nested Classes
        public new class UxmlFactory : UxmlFactory<GraphView, Graph.UxmlTraits> { }
        #endregion

        #region Fields
        public Action<ScriptableObject> OnObjectSelected;

        private GraphModel _graphModel;
        private Vector2 _lastMousePosition;
        #endregion

        #region Properties
        public GraphModel Model { get => _graphModel; }
        #endregion

        #region Constructors
        public GraphView()
        {
            InitUss();
            InitElements();
            InitManipulators();
        }
        #endregion

        #region Public Methods
        public void LoadGraphModel(GraphModel graphModel)
        {
            _graphModel = graphModel;
            SubscribeToGraphModel();
            PopulateGraphView();
        }

        public void UnloadGraphModel()
        {
            DepopulateGraphView();
            UnsubscribeFromGraphModel();
            _graphModel = null;
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList().Where(endPort =>
                endPort.direction != startPort.direction &&
                endPort.node != startPort.node).ToList();
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            if (_graphModel == null) return;
            evt.menu.AppendAction($"Create master state", (action) => RequestToCreateMasterNode());
        }
        #endregion

        #region Private Methods
        private void InitUss()
        {
            var uss = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/SingleUseWorld/Editor/StateMachine/USS/StateGraphEditorWindow.uss");
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

        private void SubscribeToGraphModel()
        {
            if (_graphModel != null)
            {
                _graphModel.AfterNodeCreated += OnAfterNodeCreated;
                _graphModel.AfterEdgeCreated += OnAfterEdgeCreated;
                _graphModel.BeforeNodeDestroyed += OnBeforeNodeDestroyed;
                _graphModel.BeforeEdgeDestroyed += OnBeforeEdgeDestroyed;
            }
            
            this.graphViewChanged += OnGraphViewChanged;
            this.RegisterCallback<MouseMoveEvent>(OnGraphMouseMove);
        }

        private void UnsubscribeFromGraphModel()
        {
            if (_graphModel != null)
            {
                _graphModel.AfterNodeCreated -= OnAfterNodeCreated;
                _graphModel.AfterEdgeCreated -= OnAfterEdgeCreated;
                _graphModel.BeforeNodeDestroyed -= OnBeforeNodeDestroyed;
                _graphModel.BeforeEdgeDestroyed -= OnBeforeEdgeDestroyed;
            }
            
            this.graphViewChanged -= OnGraphViewChanged;
            this.UnregisterCallback<MouseMoveEvent>(OnGraphMouseMove);
        }

        private void PopulateGraphView()
        {
            var initialNode = _graphModel.InitialNode;
            CreateNodeView(initialNode);

            foreach (var node in _graphModel.Nodes)
                CreateNodeView(node);

            foreach (var edge in _graphModel.Edges)
                CreateEdgeView(edge);
        }

        private void DepopulateGraphView()
        {
            this.graphViewChanged -= OnGraphViewChanged;
            this.DeleteElements(graphElements.ToList());
            this.graphViewChanged += OnGraphViewChanged;
        }

        private void CreateNodeView(NodeModel node)
        {
            var view = new NodeView();
            view.LoadNodeModel(this, node);
            view.OnNodeSelected = OnObjectSelected;

            this.graphViewChanged -= OnGraphViewChanged;
            this.AddElement(view);
            this.graphViewChanged += OnGraphViewChanged;
        }

        private void CreateEdgeView(EdgeModel edge)
        {
            var view = ConnectEdge(edge);
            view.LoadEdgeModel(this, edge);
            view.OnEdgeSelected = OnObjectSelected;

            this.graphViewChanged -= OnGraphViewChanged;
            this.AddElement(view);
            this.graphViewChanged += OnGraphViewChanged;
        }

        private void DestroyNodeView(NodeModel node)
        {
            var view = this.GetNodeByGuid(node.Guid);
            if (view == null) return;

            this.graphViewChanged -= OnGraphViewChanged;
            this.RemoveElement(view);
            this.graphViewChanged += OnGraphViewChanged;
        }

        private void DestroyEdgeView(EdgeModel edge)
        {
            var view = this.GetEdgeByGuid(edge.Guid);
            if (view == null) return;

            DisconnectEdge(view);

            this.graphViewChanged -= OnGraphViewChanged;
            this.RemoveElement(view);
            this.graphViewChanged += OnGraphViewChanged;
        }

        private EdgeView ConnectEdge(EdgeModel edge)
        {
            var sourceView = this.GetNodeByGuid(edge.Source.Guid) as NodeView;
            var targetView = this.GetNodeByGuid(edge.Target.Guid) as NodeView;
            var edgeView = sourceView.Output.ConnectTo<EdgeView>(targetView.Input);

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

        private void OnAfterNodeCreated(NodeModel node)
        {
            CreateNodeView(node);
        }

        private void OnAfterEdgeCreated(EdgeModel edge)
        {
            CreateEdgeView(edge);
        }

        private void OnBeforeNodeDestroyed(NodeModel node)
        {
            DestroyNodeView(node);
        }

        private void OnBeforeEdgeDestroyed(EdgeModel edge)
        {
            DestroyEdgeView(edge);
        }
        #endregion

        #region Internal Methods
        internal void MoveViewpointTo(Vector2 position)
        {
            // This will move the viewpoint so that the desired position
            // is in the upper left corner of the viewport.
            Vector3 viewpoint = new Vector3(-position.x, -position.y, 0);

            // To match current zoom viewpoint must be scaled.
            viewpoint = Vector3.Scale(viewpoint, this.viewTransform.scale);

            // In order for the desired position to be in the center,
            // it is necessary to shift it by half the width and height of the viewport.
            float viewportHalfWidth = this.viewport.localBound.width / 2;
            float viewportHalfHeight = this.viewport.localBound.height / 2;
            Vector3 viewportCenter = new Vector3(viewportHalfWidth, viewportHalfHeight, 0);

            this.viewTransform.position = viewpoint + viewportCenter;
        }

        internal void RequestToCreateMasterNode()
        {
            Vector2 position = this.contentViewContainer.WorldToLocal(_lastMousePosition);
            _graphModel.CreateMasterNode(position);
        }

        internal void RequestToCreateSlaveNode(NodeView nodeView)
        {
            Rect nodeViewRect = nodeView.GetPosition();
            Vector2 position = new Vector2(nodeViewRect.xMin + 20, nodeViewRect.yMin + 20);
            _graphModel.CreateSlaveNode(position, nodeView.Model);
        }

        internal void RequestToCreateEdge(Edge edge)
        {
            var sourceView = edge.output.node as NodeView;
            var targetView = edge.input.node as NodeView;
            _graphModel.CreateEdge(sourceView.Model, targetView.Model);
        }

        internal void RequestToDestroyNode(NodeView nodeView)
        {
            _graphModel.DestroyNode(nodeView.Model);
        }

        internal void RequestToDestroyEdge(EdgeView edgeView)
        {
            _graphModel.DestroyEdge(edgeView.Model);
        }
        #endregion
    }
}