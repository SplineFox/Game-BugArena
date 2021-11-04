using System;
using UnityEngine;

namespace SingleUseWorld.StateMachine.Edittime
{
    [Serializable]
    public sealed class SlaveNodeModel : NodeModel
    {
        #region Fileds
        [SerializeReference] private MasterNodeModel _master;
        #endregion

        #region Properties
        public MasterNodeModel Master { get => _master; }
        #endregion

        #region Constructors & Destructors
        public SlaveNodeModel(MasterNodeModel master, Vector2 position) : base(master.State, position)
        {
            _master = master;
        }
        #endregion

        #region Internal Methods
        internal override void OnAfterAddedToGraph()
        {
            _master.AddSlave(this);
        }

        internal override void OnBeforeRemovedFromGraph()
        {
            _master.RemoveSlave(this);
        }
        #endregion
    }
}
