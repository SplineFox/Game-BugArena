using UnityEngine;

namespace SingleUseWorld
{
    public class SkullProjectile : BaseProjectile
    {
        #region Fields
        private SkullProjectileFactory _factory;
        private SkullProjectileSettings _settings;
        #endregion

        #region Public Methods
        public void Initialize(SkullProjectileFactory factory, SkullProjectileSettings settings)
        {
            _factory = factory;
            _settings = settings;
        }

        public void Deinitialize()
        {}

        public void Launch(Vector2 direction, GameObject instigator)
        {
            StopAllCoroutines();
            elevator.height = _settings.LaunchHeight;

            var offset = new Vector3(direction.x, direction.y, 0) * _settings.LaunchOffset;
            transform.position += offset;

            var horizontalVelocity = direction * _settings.LaunchSpeed;
            _projectile.SetVelocity(horizontalVelocity);
        }
        #endregion
    }
}
