using UnityEditor;
using UnityEngine;

namespace SingleUseWorld.StateMachine.Models
{
    /// <summary>
    /// Base class of data container for graph elements.
    /// </summary>
    public abstract class GraphElementModel : ScriptableObject
    {
        #region Fields
        protected GUID _guid;
        protected GraphModel _graph;
        #endregion

        #region Properties
        public GUID Guid { get => _guid; }
        public GraphModel Graph { get => _graph; }
        #endregion

        #region Constructors
        protected void Constructor(GraphModel graph)
        {
            _graph = graph;
            _guid = GUID.Generate();
        }

        protected void Destructor()
        {
            _graph = null;
        }
        #endregion
    }
}