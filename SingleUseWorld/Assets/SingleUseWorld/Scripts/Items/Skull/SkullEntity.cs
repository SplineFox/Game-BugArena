using UnityEngine;
using UnityEngine.Assertions;

namespace SingleUseWorld
{
    public class SkullEntity : ItemEntity
    {
        #region Fields
        [SerializeField] private SkullEntityView _view = default;
        [SerializeField] private ShadowView _shadow = default;
        [SerializeField] private ProjectileTrigger _projectileTrigger = default;

        private Projectile2D _projectile;
        private SkullEntitySettings _settings;
        #endregion

        #region Properties
        public override ItemEntityType Type
        {
            get => ItemEntityType.Skull;
        }
        #endregion

        #region LifeCycle Methods
        protected override void Awake()
        {
            base.Awake();
            _projectile = GetComponent<Projectile2D>();
            Assert.IsNotNull(_projectileTrigger, "SkullEntity: ProjectileTrigger is not assigned!");
            Assert.IsNotNull(_view, "SkullEntity: SkullEntityView is not assigned!");
            Assert.IsNotNull(_shadow, "SkullEntity: ShadowView is not assigned!");
        }

        public void OnCreate(SkullEntitySettings settings)
        {
            _settings = settings;
            _projectileTrigger.EnemyHit += OnEnemyHit;
            _projectile.GroundCollision += OnGroundHit;
        }

        public void OnDestroy()
        {
            _projectileTrigger.EnemyHit -= OnEnemyHit;
            _projectile.GroundCollision -= OnGroundHit;
        }
        #endregion

        #region Public Methods
        public override void Use(Vector2 direction, GameObject instigator)
        {
            elevator.height = _settings.LaunchHeight;

            var offset = new Vector3(direction.x, direction.y, 0) * _settings.LaunchOffset;
            transform.position += offset;

            var horizontalVelocity = direction * _settings.LaunchSpeed;
            _projectile.SetVelocity(horizontalVelocity);
        }
        #endregion

        #region Private Methods
        private void OnEnemyHit(Enemy enemy)
        {
            var damageDirection = _projectile.HorizontalVelocity.normalized;
            enemy.Damage(_settings.Damage, damageDirection);

            var horizontalSpeed = Random.Range(_settings.ReboundHorizontalSpeed.start, _settings.ReboundHorizontalSpeed.end);
            var verticalSpeed = Random.Range(_settings.ReboundVerticalSpeed.start, _settings.ReboundVerticalSpeed.end);

            _projectileTrigger.enabled = false;
            _projectile.GravityScale = _settings.ReboundGravity;
            _projectile.SetVelocity(damageDirection * -horizontalSpeed, verticalSpeed);

            _shadow.Visible = false;
            
            // choose rebound angle based on hit direction 
            var angle = (damageDirection.x > 0) ? _settings.ReboundRotationAngle : -_settings.ReboundRotationAngle;
            _view.Rotate(angle, _settings.ReboundRotationTime);
            _view.FadeOut(_settings.ReboundFadeOutTime);
        }

        private void OnGroundHit()
        {
            Destroy(gameObject);
        }
        #endregion
    }
}
