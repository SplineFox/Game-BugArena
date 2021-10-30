using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SingleUseWorld.StateMachine.EditorTime
{
    /// <summary>
    /// Data container for nodes and edges
    /// that represent states and transitions respectively
    /// and used as a blueprints for runtime state machine.
    /// </summary>
    public class GraphModel : ScriptableObject
    {
        #region Fields
        [SerializeField] private List<StateModel> _states = new List<StateModel>();
        [SerializeField] private List<TransitionModel> _transitions = new List<TransitionModel>();

        [SerializeReference] private NodeModel _initialNode = null;
        [SerializeReference] private List<NodeModel> _nodes = new List<NodeModel>();
        [SerializeReference] private List<EdgeModel> _edges = new List<EdgeModel>();
        #endregion

        #region Properties
        public StateModel InitialState { get => _initialNode.State; }
        public NodeModel InitialNode { get => _initialNode; }
        public IReadOnlyList<StateModel> States { get => _states; }
        public IReadOnlyList<TransitionModel> Transitions { get => _transitions; }
        public IReadOnlyList<NodeModel> Nodes { get => _nodes; }
        public IReadOnlyList<EdgeModel> Edges { get => _edges; }
        #endregion

        #region Delegates & Events
        public delegate void AfterNodeCreatedDelegate(NodeModel node);
        public delegate void BeforeNodeDestroyedDelegate(NodeModel node);

        public delegate void AfterEdgeCreatedDelegate(EdgeModel edge);
        public delegate void BeforeEdgeDestroyedDelegate(EdgeModel edge);
        
        public event AfterNodeCreatedDelegate AfterNodeCreated = delegate { };
        public event BeforeNodeDestroyedDelegate BeforeNodeDestroyed = delegate { };

        public event AfterEdgeCreatedDelegate AfterEdgeCreated = delegate { };
        public event BeforeEdgeDestroyedDelegate BeforeEdgeDestroyed = delegate { };

        #endregion

        #region Public Methods
        /// <summary>
        /// Creates initial node as well as its state.
        /// </summary>
        public void CreateInitialNode()
        {
            // If we already created initial node
            if (_initialNode != null)
                return;

            // Create state
            var state = StateModel.New();
            AddObj(state);

            // Create node
            _initialNode = new InitialNodeModel(state, Vector2.zero);
            _initialNode.Graph = this;

            // Notify
            _initialNode.OnAfterAddedToGraph();

            // Force Unity save changes
            EditorUtility.SetDirty(this);
        }

        /// <summary>
        /// Creates master node as well as its state.
        /// </summary>
        public void CreateMasterNode(Vector2 position)
        {
            // Create state
            var state = StateModel.New();
            AddObj(state);
            SetUniqueNameFor(state);
            _states.Add(state);
            
            // Create node
            var master = new MasterNodeModel(state, position);
            master.Graph = this;
            _nodes.Add(master);

            // Notify
            master.OnAfterAddedToGraph();
            AfterNodeCreated.Invoke(master);

            // Force Unity save changes
            EditorUtility.SetDirty(this);
        }

        /// <summary>
        /// Creates slave node.
        /// </summary>
        public void CreateSlaveNode(Vector2 position, NodeModel node)
        {
            // Check conditions
            var master = node as MasterNodeModel;
            if (master == null) return;

            // Create node
            var slave = new SlaveNodeModel(master, position);
            slave.Graph = this;
            _nodes.Add(slave);

            // Notify
            slave.OnAfterAddedToGraph();
            AfterNodeCreated.Invoke(slave);

            // Force Unity save changes
            EditorUtility.SetDirty(this);
        }

        /// <summary>
        /// Creates edge as well as its transition.
        /// </summary>
        public void CreateEdge(NodeModel source, NodeModel target)
        {
            // Check conditions
            var initial = source as InitialNodeModel;
            var master = source as MasterNodeModel;
            var slave = target as SlaveNodeModel;

            // If not "intial -> slave" situation
            if (initial == null || slave == null)
            {
                // If not "master -> slave" situation
                if (master == null || slave == null) return;
                // If "master -> slave" situation but with same state or nodes already have an edge
                if (master.State == slave.State || source.HasOutputWithState(target.State)) return;
            }

            // Create transition
            var transition = TransitionModel.New(source.State, target.State);
            AddObj(transition);
            _transitions.Add(transition);

            // Create edge
            var edge = new EdgeModel(transition, source, target);
            edge.Graph = this;
            _edges.Add(edge);

            // Notify
            edge.OnAfterAddedToGraph();
            AfterEdgeCreated.Invoke(edge);

            // Force Unity save changes
            EditorUtility.SetDirty(this);
        }

        /// <summary>
        /// Destroys given node.
        /// </summary>
        public void DestroyNode(NodeModel node)
        {
            if (node is MasterNodeModel master)
                DestroyMasterNode(master);

            if (node is SlaveNodeModel slave)
                DestroySlaveNode(slave);
        }

        /// <summary>
        /// Destroys edge as well as its transition.
        /// </summary>
        public void DestroyEdge(EdgeModel edge)
        {
            // Notify
            BeforeEdgeDestroyed.Invoke(edge);
            edge.OnBeforeRemovedFromGraph();

            // Destroy edge
            _edges.Remove(edge);
            edge.Graph = null;

            // Destroy transition
            _transitions.Remove(edge.Transition);
            RemoveObj(edge.Transition);
            TransitionModel.Delete(edge.Transition);

            // Force Unity save changes
            EditorUtility.SetDirty(this);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Destroys master node as well as its slaves, edges and state.
        /// </summary>
        private void DestroyMasterNode(MasterNodeModel master)
        {
            // Destroy slaves
            var slavesToRemove = new List<SlaveNodeModel>(master.Slaves);
            foreach (var slave in slavesToRemove)
                DestroySlaveNode(slave);

            // Destroy edges
            var edgesToRemove = new List<EdgeModel>(master.Outputs);
            foreach (var edge in edgesToRemove)
                DestroyEdge(edge);

            // Notify
            BeforeNodeDestroyed.Invoke(master);
            master.OnBeforeRemovedFromGraph();

            // Destroy node
            _nodes.Remove(master);
            master.Graph = null;

            // Destroy state
            _states.Remove(master.State);
            RemoveObj(master.State);
            StateModel.Delete(master.State);

            // Force Unity save changes
            EditorUtility.SetDirty(this);
        }

        /// <summary>
        /// Destroys slave node as well as its edges.
        /// </summary>
        private void DestroySlaveNode(SlaveNodeModel slave)
        {
            // Destroy edges
            var edgesToRemove = new List<EdgeModel>(slave.Inputs);
            foreach (var edge in edgesToRemove)
                DestroyEdge(edge);

            // Notify
            BeforeNodeDestroyed.Invoke(slave);
            slave.OnBeforeRemovedFromGraph();

            // Destroy node
            _nodes.Remove(slave);
            slave.Graph = null;

            // Force Unity save changes
            EditorUtility.SetDirty(this);
        }

        /// <summary>
        /// Adds given object to this asset.
        /// </summary>
        private void AddObj(ScriptableObject obj)
        {
            AssetDatabase.AddObjectToAsset(obj, this);
            AssetDatabase.SaveAssets();
        }

        /// <summary>
        /// Removes given object from this asset.
        /// </summary>
        private void RemoveObj(ScriptableObject obj)
        {
            AssetDatabase.RemoveObjectFromAsset(obj);
            AssetDatabase.SaveAssets();
        }

        private void SetUniqueNameFor(StateModel state)
        {
            var existingNames = _states.Select(stateModel => stateModel.Name).ToArray();
            var baseName = state.Name;
            var uniqueName = ObjectNames.GetUniqueName(existingNames, baseName);
            state.Name = uniqueName;
        }
        #endregion
    }
}