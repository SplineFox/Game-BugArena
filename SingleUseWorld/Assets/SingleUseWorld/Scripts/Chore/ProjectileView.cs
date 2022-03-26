using UnityEngine;

namespace SingleUseWorld
{
    public abstract class ProjectileView : MonoBehaviour
    {
        #region Properties
        protected abstract IBodySubview _body { get; }
        protected abstract IShadowSubview _shadow { get; }
        #endregion

        #region Public Methods
        public virtual void UpdateHeightPresentation(float height)
        {
            _body.UpdateHeightPresentation(height);
            _shadow.UpdateHeightPresentation(height);
        }
        #endregion
    }
}