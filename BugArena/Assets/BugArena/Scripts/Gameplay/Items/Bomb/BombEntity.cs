using UnityEngine;

namespace BugArena
{
    public class BombEntity : ItemEntity
    {
        #region Fields
        private Projectile2D _projectile;
        private BombEntitySettings _settings;

        private IEffectSpawner _effectSpawner;
        private IHitTimer _hitTimer;
        private CameraShaker _cameraShaker;
        private Score _score;
        private int _hitCombo;
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

        public void OnCreate(BombEntitySettings settings, Score score, IEffectSpawner effectSpawner, IHitTimer hitTimer, CameraShaker cameraShaker)
        {
            _settings = settings;
            _score = score;
            _effectSpawner = effectSpawner;
            _hitTimer = hitTimer;
            _cameraShaker = cameraShaker;

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
            _effectSpawner.SpawnEffect(ComplexEffectType.Explosion, transform.position);
            Destroy(gameObject);
        }

        private void CheckDamageZone()
        {
            var scorePoints = 0;
            var collisions = Physics2D.OverlapCircleAll(transform.position, _settings.DamageRadius, _settings.DamageMask);
            foreach (var collision in collisions)
            {
                if (collision.gameObject.TryGetComponent<Enemy>(out var enemy))
                {
                    // distance
                    var vectorToEnemy = enemy.transform.position - transform.position;
                    var distanceInterpolant = Mathf.InverseLerp(0f, _settings.DamageRadius, vectorToEnemy.magnitude);
                    
                    // damage
                    var damageAmount = _settings.DamageAmount;
                    var damageDirection = vectorToEnemy.normalized;

                    // knockback
                    var verticalKnockback = _settings.KnockbackVerticalSpeed.GetLerpedValue(distanceInterpolant);
                    var horizontalKnockback = damageDirection * _settings.KnockbackHorizontalSpeed.GetLerpedValue(distanceInterpolant);
                    var spinKnockback = -Mathf.Sign(damageDirection.x) * _settings.KnockbackSpinSpeed.GetLerpedValue(distanceInterpolant);

                    var damage = new Damage(damageAmount, damageDirection, horizontalKnockback, verticalKnockback, spinKnockback);
                    enemy.TakeDamage(damage);

                    _hitCombo++;
                    scorePoints += enemy.PointsPerKill;
                }
            }
            if(_hitCombo != 0)
            {
                int multiplier = Mathf.Min(_hitCombo, 5);
                _score.AddPoints(scorePoints * multiplier);
                _hitTimer.StopTime(0.1f);
            }
            _cameraShaker.Shake(5f, 0.4f);
        }
        #endregion
    }
}