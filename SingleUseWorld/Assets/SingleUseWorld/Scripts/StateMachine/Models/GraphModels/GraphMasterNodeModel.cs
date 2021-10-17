using System.Collections.Generic;
using UnityEngine;

namespace SingleUseWorld.StateMachine.Models
{
    /// <summary>
    /// Data container for state-node element used as input in graph.
    /// </summary>
    public sealed class GraphMasterNodeModel : GraphNodeModel
    {
        #region Fileds
        private List<GraphSlaveNodeModel> _slaves;
        #endregion

        #region Properties
        public IReadOnlyList<GraphSlaveNodeModel> Slaves { get => _slaves; }
        #endregion

        #region Constructors & Destructors
        private void Constructor(GraphModel graph, Vector2 position, StateModel state)
        {
            base.Constructor(graph, position, state);
            _slaves = new List<GraphSlaveNodeModel>();
        }

        private void Destructor()
        {
            base.Destructor();
            _slaves = null;
        }
        #endregion

        #region Public Methods
        internal void AddSlave(GraphSlaveNodeModel slave)
        {
            _slaves.Add(slave);
        }

        internal void RemoveSlave(GraphSlaveNodeModel slave)
        {
            _slaves.Remove(slave);
        }
        #endregion

        #region Static Methods
        public static GraphMasterNodeModel New(GraphModel graph, Vector2 position, StateModel state)
        {
            var obj = CreateInstance<GraphMasterNodeModel>();
            obj.Constructor(graph, position, state);
            return obj;
        }

        public static void Delete(GraphMasterNodeModel obj)
        {
            obj.Destructor();
            DestroyImmediate(obj);
        }
        #endregion
    }
}
