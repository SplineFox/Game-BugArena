using UnityEditor;
using UnityEngine;

namespace SingleUseWorld.StateMachine.Models
{
    /// <summary>
    /// Data container for transition-edge element.
    /// </summary>
    public sealed class GraphEdgeModel : GraphElementModel
    {
        #region Fields
        private GraphNodeModel _source;
        private GraphNodeModel _target;
        private TransitionModel _transition;
        #endregion

        #region Properties
        public GraphNodeModel Source { get => _source; }
        public GraphNodeModel Target { get => _target; }
        public TransitionModel Transition { get => _transition; }
        #endregion

        #region Constructors & Destructors
        private void Constructor(GraphModel graph, GraphNodeModel source, GraphNodeModel target, TransitionModel transition)
        {
            base.Constructor(graph);
            _source = source;
            _target = target;
            _transition = transition;
            _source.AddOutput(this);
            _target.AddInput(this);
        }

        private void Destructor()
        {
            base.Destructor();
            _source.RemoveOutput(this);
            _target.RemoveInput(this);
            _source = null;
            _target = null;
            _transition = null;
        }
        #endregion

        #region Static Methods
        public static GraphEdgeModel New(GraphModel graph, GraphNodeModel source, GraphNodeModel target, TransitionModel transition)
        {
            var obj = CreateInstance<GraphEdgeModel>();
            obj.Constructor(graph, source, target, transition);
            return obj;
        }

        public static void Delete(GraphEdgeModel obj)
        {
            obj.Destructor();
            DestroyImmediate(obj);
        }
        #endregion
    }
}