using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace SingleUseWorld
{
    public class ArrowEntity : ItemEntity
    {
        #region Fields
        [SerializeField] private ProjectileTrigger _projectileTrigger = default;

        private Projectile2D _projectile;
        private ArrowEntitySettings _settings;
        #endregion

        #region Properties
        public override ItemEntityType Type
        {
            get => ItemEntityType.Arrow;
        }
        #endregion

        #region LifeCycle Methods
        protected override void Awake()
        {
            base.Awake();
            _projectile = GetComponent<Projectile2D>();
            Assert.IsNotNull(_projectileTrigger, "ArrowEntity: ProjectileTrigger is not assigned!");
        }

        public void OnCreate(ArrowEntitySettings settings)
        {
            _settings = settings;
            _projectile.WallCollision += OnWallHit;
            _projectileTrigger.EnemyHit += OnEnemyHit;
        }

        public void OnDestroy()
        {
            _projectile.WallCollision -= OnWallHit;
            _projectileTrigger.EnemyHit -= OnEnemyHit;
        }
        #endregion

        #region Public Methods
        public override void Use(Vector2 direction, GameObject instigator)
        {
            elevator.height = _settings.LaunchHeight;

            var offset = new Vector3(direction.x, direction.y, 0) * _settings.LaunchOffset;
            transform.position += offset;
            transform.right = direction;

            var horizontalVelocity = direction * _settings.LaunchSpeed;
            _projectile.SetVelocity(horizontalVelocity);
        }
        #endregion

        #region Private Methods
        private void OnEnemyHit(Enemy enemy)
        {
            // damage
            var damageAmount = _settings.DamageAmount;
            var damageDirection = _projectile.HorizontalVelocity.normalized;

            // knockback
            var verticalKnockback = _settings.KnockbackVerticalSpeed.GetRandomValue();
            var horizontalKnockback = damageDirection * _settings.KnockbackHorizontalSpeed.GetRandomValue();
            var spinKnockback = -Mathf.Sign(damageDirection.x) * _settings.KnockbackSpinSpeed.GetRandomValue();

            var damage = new Damage(damageAmount, damageDirection, horizontalKnockback, verticalKnockback, spinKnockback);
            enemy.TakeDamage(damage);
        }

        private void OnWallHit()
        {
            Destroy(gameObject);
        }
        #endregion
    }
}