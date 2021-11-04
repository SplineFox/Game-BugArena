using System.Collections.Generic;
using UnityEngine;

namespace SingleUseWorld.StateMachine.Edittime
{
    /// <summary>
    /// Data container for state-node element used as input in graph.
    /// </summary>
    public sealed class MasterNodeModel : NodeModel
    {
        #region Fileds
        [SerializeReference] private List<SlaveNodeModel> _slaves;
        #endregion

        #region Properties
        public IReadOnlyList<SlaveNodeModel> Slaves { get => _slaves; }
        #endregion

        #region Constructors
        public MasterNodeModel(StateModel state, Vector2 position) : base(state, position)
        {
            _slaves = new List<SlaveNodeModel>();
        }
        #endregion

        #region Internal Methods
        internal void AddSlave(SlaveNodeModel slave)
        {
            _slaves.Add(slave);
        }

        internal void RemoveSlave(SlaveNodeModel slave)
        {
            _slaves.Remove(slave);
        }
        #endregion
    }
}
