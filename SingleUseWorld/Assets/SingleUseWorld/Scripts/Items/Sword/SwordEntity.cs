using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace SingleUseWorld
{
    public class SwordEntity : ItemEntity
    {
        #region Fields
        [SerializeField] private ProjectileTrigger _projectileTrigger = default;

        private SwordEntitySettings _settings;

        private Score _score;
        private IHitTimer _hitTimer;
        private CameraShaker _cameraShaker;
        private int _hitCombo;
        #endregion

        #region Properties
        public override ItemEntityType Type
        {
            get => ItemEntityType.LongSword;
        }
        #endregion

        #region LifeCycle Methods
        protected override void Awake()
        {
            base.Awake();
            Assert.IsNotNull(_projectileTrigger, "SwordEntity: ProjectileTrigger is not assigned!");
        }

        public void OnCreate(SwordEntitySettings settings, Score score, IHitTimer hitTimer, CameraShaker cameraShaker)
        {
            _settings = settings;
            _score = score;
            _hitTimer = hitTimer;
            _cameraShaker = cameraShaker;

            _projectileTrigger.EnemyHit += OnEnemyHit;
        }

        public void OnDestroy()
        {
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

            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var angleFrom = angle - _settings.RotationAngle / 2;

            transform.eulerAngles = angleFrom * Vector3.forward;
            StartCoroutine(RotateTo(_settings.RotationAngle, _settings.RotationDuration));
        }
        #endregion

        #region Private Methods
        private void OnEnemyHit(Enemy enemy)
        {
            // damage
            var damageAmount = _settings.DamageAmount;
            var damageDirection = transform.up;

            // knockback
            var verticalKnockback = _settings.KnockbackVerticalSpeed.GetRandomValue();
            var horizontalKnockback = damageDirection * _settings.KnockbackHorizontalSpeed.GetRandomValue();
            var spinKnockback = -Mathf.Sign(damageDirection.x) * _settings.KnockbackSpinSpeed.GetRandomValue();

            var damage = new Damage(damageAmount, damageDirection, horizontalKnockback, verticalKnockback, spinKnockback);
            enemy.TakeDamage(damage);

            _hitCombo++;
            int multiplier = Mathf.Min(_hitCombo, 4);
            _score.AddPoints(enemy.PointsPerKill * multiplier);

            if(_hitCombo == 1)
            {
                _hitTimer.StopTime(0.1f);
                _cameraShaker.Shake(3f, 0.1f);
            }
        }

        private IEnumerator RotateTo(float angle, float duration)
        {
            var initalRotation = Vector3.forward * transform.eulerAngles.z;
            var targetRotation = Vector3.forward * (initalRotation.z + angle);

            var elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                transform.eulerAngles = Vector3.Lerp(initalRotation, targetRotation, elapsedTime / duration);
                yield return null;
            }
            Destroy(gameObject);
        }
        #endregion
    }
}