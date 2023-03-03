using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "SwordEntityFactorySO", menuName = "SingleUseWorld/Factories/Items/SwordEntity Factory SO")]
    public class SwordEntityFactory : ScriptableFactory, IMonoFactory<ItemEntity>
    {
        [SerializeField] private SwordEntity _swordEntityPrefab;
        [SerializeField] private SwordEntitySettings _swordEntitySettings;

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
            var swordEntity = CreateInstance<SwordEntity>(_swordEntityPrefab);
            swordEntity.OnCreate(_swordEntitySettings, _score, _hitTimer, _cameraShaker);
            return swordEntity;
        }
        #endregion
    }
}
