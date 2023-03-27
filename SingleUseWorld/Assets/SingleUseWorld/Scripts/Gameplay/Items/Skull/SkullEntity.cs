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

        private bool _hitEnemy;
        private Projectile2D _projectile;
        private SkullEntitySettings _settings;

        private Score _score;
        private IHitTimer _hitTimer;
        private CameraShaker _cameraShaker;
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

        public void OnCreate(SkullEntitySettings settings, Score score, IHitTimer hitTimer, CameraShaker cameraShaker)
        {
            _settings = settings;
            _score = score;
            _hitTimer = hitTimer;
            _cameraShaker = cameraShaker;

            _hitEnemy = false;
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
            if (_hitEnemy)
                return;
            else
                _hitEnemy = true;

            // damage
            var damageAmount = _settings.DamageAmount;
            var damageDirection = _projectile.HorizontalVelocity.normalized;
            
            // knockback
            var verticalKnockback = _settings.KnockbackVerticalSpeed.GetRandomValue();
            var horizontalKnockback = damageDirection * _settings.KnockbackHorizontalSpeed.GetRandomValue();
            var spinKnockback = -Mathf.Sign(damageDirection.x) * _settings.KnockbackSpinSpeed.GetRandomValue();

            var damage = new Damage(damageAmount, damageDirection, horizontalKnockback, verticalKnockback, spinKnockback);
            enemy.TakeDamage(damage);
            _score.AddPoints(enemy.PointsPerKill);
            _hitTimer.StopTime(0.1f);
            _cameraShaker.Shake(1.8f, 0.1f);

            // rebound
            var verticalRebound = _settings.ReboundVerticalSpeed.GetRandomValue();
            var horizontalRebound = damageDirection * -_settings.ReboundHorizontalSpeed.GetRandomValue();
            var spinRebound = Mathf.Sign(damageDirection.x) * _settings.ReboundSpinSpeed;

            _projectileTrigger.enabled = false;
            _projectile.GravityScale = _settings.ReboundGravityScale;
            _projectile.SetVelocity(horizontalRebound, verticalRebound);

            _shadow.Visible = false;
            _view.StartSpin(spinRebound);
            _view.FadeOut(_settings.ReboundFadeOutTime);
        }

        private void OnGroundHit()
        {
            _view.StopSpin();
            Destroy(gameObject);
        }
        #endregion
    }
}
