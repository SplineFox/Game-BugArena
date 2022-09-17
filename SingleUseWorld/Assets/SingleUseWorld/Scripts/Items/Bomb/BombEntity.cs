using UnityEngine;

namespace SingleUseWorld
{
    public class BombEntity : ItemEntity
    {
        #region Fields
        private Projectile2D _projectile;
        private BombEntitySettings _settings;
        #endregion

        #region Properties
        public override ItemEntityType Type
        {
            get => ItemEntityType.Bomb;
        }
        #endregion

        #region LifeCycle Methods
        protected override void Awake()
        {
            base.Awake();
            _projectile = GetComponent<Projectile2D>();
        }

        public void OnCreate(BombEntitySettings settings)
        {
            _settings = settings;
            _projectile.GroundCollision += OnGroundHit;
        }

        public void OnDestroy()
        {
            _projectile.GroundCollision -= OnGroundHit;
        }
        #endregion

        #region Public Methods
        public override void Use(Vector2 direction, GameObject instigator)
        {
            elevator.height = _settings.LaunchHeight;

            var offset = new Vector3(direction.x, direction.y, 0) * _settings.LaunchOffset;
            transform.position += offset;

            var horizontalVelocity = direction * _settings.LaunchHorizontalSpeed;
            var verticalVelocity = _settings.LaunchVerticalSpeed;
            _projectile.GravityScale = _settings.LaunchGravity;
            _projectile.SetVelocity(horizontalVelocity, verticalVelocity);
        }
        #endregion

        #region Private Methods
        private void OnGroundHit()
        {
            CheckDamageZone();
            Destroy(gameObject);
        }

        private void CheckDamageZone()
        {
            var collisions = Physics2D.OverlapCircleAll(transform.position, _settings.DamageRadius, _settings.DamageMask);
            foreach (var collision in collisions)
            {
                if (collision.gameObject.TryGetComponent<Enemy>(out var enemy))
                {
                    var damageDirection = enemy.transform.position - transform.position;
                    enemy.Damage(_settings.Damage, damageDirection);
                }
            }
        }
        #endregion
    }
}