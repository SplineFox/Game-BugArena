using UnityEngine;

namespace SingleUseWorld
{
    public class SkullEntity : ItemEntity
    {
        #region Fields
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
        }

        public void OnCreate(SkullEntitySettings settings)
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
            StopAllCoroutines();
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
        }
        #endregion
    }
}
