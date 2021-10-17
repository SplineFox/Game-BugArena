using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace SingleUseWorld.StateMachine.Models
{
    /// <summary>
    /// Data container for nodes and edges
    /// that represent states and transitions respectively
    /// and used as a blueprints for runtime state machine.
    /// </summary>
    [CreateAssetMenu(fileName = "New StateGraph",menuName = "SingleUseWorld/StateMachine/Create StateGraph")]
    public class GraphModel : ScriptableObject
    {
        #region Fields
        private List<StateModel> _states = new List<StateModel>();
        private List<TransitionModel> _transitions = new List<TransitionModel>();
        private List<GraphNodeModel> _nodes = new List<GraphNodeModel>();
        private List<GraphEdgeModel> _edges = new List<GraphEdgeModel>();
        #endregion

        #region Properties
        public IReadOnlyList<StateModel> States { get => _states; }
        public IReadOnlyList<TransitionModel> Transitions { get => _transitions; }
        public IReadOnlyList<GraphNodeModel> Nodes { get => _nodes; }
        public IReadOnlyList<GraphEdgeModel> Edges { get => _edges; }
        #endregion

        #region Delegates & Events
        public delegate void NodeCreatedDelegate(GraphNodeModel node);
        public delegate void NodeAboutToBeDestroyedDelegate(GraphNodeModel node);

        public delegate void EdgeCreatedDelegate(GraphEdgeModel edge);
        public delegate void EdgeAboutToBeDestroyedDelegate(GraphEdgeModel edge);

        public event NodeCreatedDelegate NodeCreated = delegate { };
        public event NodeAboutToBeDestroyedDelegate NodeAboutToBeDestroyed = delegate { };

        public event EdgeCreatedDelegate EdgeCreated = delegate { };
        public event EdgeAboutToBeDestroyedDelegate EdgeAboutToBeDestroyed = delegate { };

        #endregion

        #region Public Methods
        /// <summary>
        /// Creates master node as well as its state.
        /// </summary>
        public void CreateMasterNode(Vector2 position)
        {
            var state = StateModel.New();
            AddObj(state);
            _states.Add(state);
            
            var master = GraphMasterNodeModel.New(this, position, state);
            AddObj(master);
            _nodes.Add(master);

            NodeCreated.Invoke(master);
        }

        /// <summary>
        /// Creates slave node.
        /// </summary>
        public void CreateSlaveNode(Vector2 position, GraphNodeModel node)
        {
            var master = node as GraphMasterNodeModel;
            if (!master) return;

            var slave = GraphSlaveNodeModel.New(this, position, master);
            AddObj(slave);
            _nodes.Add(slave);
            
            NodeCreated.Invoke(slave);
        }

        /// <summary>
        /// Creates edge as well as its transition.
        /// </summary>
        public void CreateEdge(GraphNodeModel source, GraphNodeModel target)
        {
            var master = source as GraphMasterNodeModel;
            var slave = target as GraphSlaveNodeModel;
            if (!master || !slave) return;

            var transition = TransitionModel.New(target.State);
            AddObj(transition);
            _transitions.Add(transition);

            source.State.AddTransition(transition);

            var edge = GraphEdgeModel.New(this, source, target, transition);
            AddObj(edge);
            _edges.Add(edge);

            EdgeCreated.Invoke(edge);
        }

        /// <summary>
        /// Destroys given node.
        /// </summary>
        public void DestroyNode(GraphNodeModel node)
        {
            if (node is GraphMasterNodeModel master)
                DestroyMasterNode(master);

            if (node is GraphSlaveNodeModel slave)
                DestroySlaveNode(slave);
        }

        /// <summary>
        /// Destroys edge as well as its transition.
        /// </summary>
        public void DestroyEdge(GraphEdgeModel edge)
        {
            EdgeAboutToBeDestroyed.Invoke(edge);

            edge.Source.State.RemoveTransition(edge.Transition);

            _transitions.Remove(edge.Transition);
            RemoveObj(edge.Transition);
            TransitionModel.Delete(edge.Transition);

            _edges.Remove(edge);
            RemoveObj(edge);
            GraphEdgeModel.Delete(edge);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Destroys master node as well as its slaves, edges and state.
        /// </summary>
        private void DestroyMasterNode(GraphMasterNodeModel master)
        {
            foreach (var slave in master.Slaves)
                DestroySlaveNode(slave);

            foreach (var edge in master.Outputs)
                DestroyEdge(edge);

            NodeAboutToBeDestroyed.Invoke(master);

            _states.Remove(master.State);
            RemoveObj(master.State);
            StateModel.Delete(master.State);

            _nodes.Remove(master);
            RemoveObj(master);
            GraphMasterNodeModel.Delete(master);
        }

        /// <summary>
        /// Destroys slave node as well as its edges.
        /// </summary>
        private void DestroySlaveNode(GraphSlaveNodeModel slave)
        {
            foreach (var edge in slave.Inputs)
                DestroyEdge(edge);

            NodeAboutToBeDestroyed.Invoke(slave);

            _nodes.Remove(slave);
            RemoveObj(slave);
            GraphSlaveNodeModel.Delete(slave);
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
        #endregion
    }
}