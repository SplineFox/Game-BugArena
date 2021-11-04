using System;
using UnityEngine;
using UnityEditor;

namespace SingleUseWorld.StateMachine.Edittime
{
    [Serializable]
    public abstract class GraphElementModel
    {
        #region Fields
        [SerializeField] private string _guid;
        [SerializeField] private GraphModel _graph;
        #endregion

        #region Properties
        public string Guid { get => _guid; }
        public GraphModel Graph { get => _graph; internal set => _graph = value; }
        #endregion

        #region Constructors
        public GraphElementModel()
        {
            _graph = null;
            _guid = GUID.Generate().ToString();
        }
        #endregion

        #region Internal Methods
        internal virtual void OnAfterAddedToGraph() { }
        internal virtual void OnBeforeRemovedFromGraph() { }
        #endregion
    }
}