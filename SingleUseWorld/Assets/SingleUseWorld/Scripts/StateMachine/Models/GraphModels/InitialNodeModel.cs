using UnityEngine;

namespace SingleUseWorld.StateMachine.Models
{
    public class InitialNodeModel : NodeModel
    {
        #region Constants
        private const string DEFAULT_NAME = "Entry";
        #endregion

        #region Constructors
        public InitialNodeModel(StateModel state, Vector2 position) : base(state, position)
        {
            state.Name = DEFAULT_NAME;
            state.Color = new Color32(40, 124, 60, 255);
        }
        #endregion
    }
}
