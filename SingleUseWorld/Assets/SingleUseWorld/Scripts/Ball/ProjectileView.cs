using UnityEngine;

namespace SingleUseWorld
{
    public class ProjectileView : MonoBehaviour
    {
        #region Fields
        [SerializeField] private BodySubview _body = default;
        [SerializeField] private ShadowSubview _shadow = default;
        #endregion

        #region Public Methods
        public void UpdateHeightPresentation(float height)
        {
            _body.UpdateHeightPresentation(height);
            _shadow.UpdateHeightPresentation(height);
        }
        #endregion
    }
}