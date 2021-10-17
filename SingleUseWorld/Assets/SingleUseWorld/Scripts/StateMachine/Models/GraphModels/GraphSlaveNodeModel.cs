using UnityEngine;

namespace SingleUseWorld.StateMachine.Models
{
    /// <summary>
    /// Data container for state-node element used as output in graph.
    /// </summary>
    public sealed class GraphSlaveNodeModel : GraphNodeModel
    {
        #region Fileds
        private GraphMasterNodeModel _master;
        #endregion

        #region Properties
        public GraphMasterNodeModel Master { get => _master; }
        #endregion

        #region Constructors & Destructors
        private void Constructor(GraphModel graph, Vector2 position, GraphMasterNodeModel master)
        {
            base.Constructor(graph, position, master.State);
            _master = master;
            _master.AddSlave(this);
        }

        private void Destructor()
        {
            base.Destructor();
            _master.RemoveSlave(this);
            _master = null;
        }
        #endregion

        #region Static Methods
        public static GraphSlaveNodeModel New(GraphModel graph, Vector2 position, GraphMasterNodeModel master)
        {
            var obj = CreateInstance<GraphSlaveNodeModel>();
            obj.Constructor(graph, position, master);
            return obj;
        }

        public static void Delete(GraphSlaveNodeModel obj)
        {
            obj.Destructor();
            DestroyImmediate(obj);
        }
        #endregion
    }
}
