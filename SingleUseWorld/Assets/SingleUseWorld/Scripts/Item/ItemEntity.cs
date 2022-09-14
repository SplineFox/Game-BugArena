using UnityEngine;

namespace SingleUseWorld
{
    public abstract class ItemEntity : BaseBehaviour
    {
        #region Fields
        #endregion

        #region Properties
        public abstract ItemEntityType Type { get; }
        #endregion

        #region Delegates & Events
        #endregion

        #region Constructors
        #endregion

        #region LifeCycle Methods
        #endregion

        #region Public Methods
        public abstract void Use(Vector2 direction, GameObject instigator);
        #endregion
	
    	#region Internal Methods
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        #endregion
    }
}