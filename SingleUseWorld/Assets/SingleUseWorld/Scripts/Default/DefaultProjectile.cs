using UnityEngine;

namespace SingleUseWorld
{
    public class DefaultProjectile : BaseProjectile
    {
        #region Fields
        [SerializeField]
        private float _speed = 5f;

        [SerializeField]
        private Vector2 _direction = Vector2.zero;
        #endregion

        #region LifeCycle Methods
        private void Start()
        {
            Initialize();
        }
        #endregion

        #region Public Methods
        public override void Initialize()
        {
            base.Initialize();
            var velocity = _direction.normalized * _speed;
            _projectile.SetVelocity(velocity);
        }
        #endregion
    }
}