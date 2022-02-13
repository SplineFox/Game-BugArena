using UnityEngine;

namespace SingleUseWorld
{
    public class ItemView : ProjectileView
    {
        #region Fields
        [SerializeField] private BodySubview _bodySubview = default;
        [SerializeField] private ShadowSubview _shadowSubview = default;
        #endregion

        #region Properties
        protected override IBodySubview _body => _bodySubview;
        protected override IShadowSubview _shadow => _shadowSubview;
        #endregion
    }
}