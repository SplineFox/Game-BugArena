using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "SkullEntityFactorySO", menuName = "SingleUseWorld/Factories/Items/SkullEntity Factory SO")]
    public class SkullEntityFactory : ScriptableFactory, IMonoFactory<ItemEntity>
    {
        [SerializeField] private SkullEntity _skullEntityPrefab;
        [SerializeField] private SkullEntitySettings _skullEntitySettings;

        private Score _score;
        private HitTimer _hitTimer;
        private CameraShaker _cameraShaker;

        #region Public Mehods
        public void Inject(Score score, HitTimer hitTimer, CameraShaker cameraShaker)
        {
            _score = score;
            _hitTimer = hitTimer;
            _cameraShaker = cameraShaker;
        }

        public ItemEntity Create()
        {
            var skullEntity = CreateInstance<SkullEntity>(_skullEntityPrefab);
            skullEntity.OnCreate(_skullEntitySettings, _score, _hitTimer, _cameraShaker);
            return skullEntity;
        }
        #endregion
    }
}
