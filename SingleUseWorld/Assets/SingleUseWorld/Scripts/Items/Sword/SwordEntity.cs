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

        public void OnCreate(SwordEntitySettings settings)
        {
            _settings = settings;
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
            var direction = transform.right;
            enemy.Damage(_settings.Damage, direction);
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