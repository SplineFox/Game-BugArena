using UnityEngine;

namespace SingleUseWorld
{
    public class PlayerView : ProjectileView
    {
        #region Fields
        [SerializeField] private PlayerBodySubview _playerBody = default;
        [SerializeField] private ShadowSubview _playerShadow = default;
        #endregion

        #region Properties
        protected override IBodySubview _body => _playerBody;

        protected override IShadowSubview _shadow => _playerShadow;
        #endregion

        #region Public Methods
        public void SetDirectionParameter(Vector2 direction)
        {
            _playerBody.SetDirectionParameter(direction);
        }

        public void PlayIdleAnimation()
        {
            _playerBody.PlayIdleAnimation();
        }

        public void PlayMoveAnimation()
        {
            _playerBody.PlayMoveAnimation();
        }

        public void PlayIdleCarryAnimation()
        {
            _playerBody.PlayIdleCarryAnimation();
        }

        public void PlayMoveCarryAnimation()
        {
            _playerBody.PlayMoveCarryAnimation();
        }

        public void PlayThrowAnimation()
        {
            _playerBody.PlayThrowAnimation();
        }
        #endregion
    }
}