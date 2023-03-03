using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "ArrowEntityFactorySO", menuName = "SingleUseWorld/Factories/Items/ArrowEntity Factory SO")]
    public class ArrowEntityFactory : ScriptableFactory, IMonoFactory<ItemEntity>
    {
        [SerializeField] private ArrowEntity _arowEntityPrefab;
        [SerializeField] private ArrowEntitySettings _arowEntitySettings;

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
            var arrowEntity = CreateInstance<ArrowEntity>(_arowEntityPrefab);
            arrowEntity.OnCreate(_arowEntitySettings, _score, _hitTimer, _cameraShaker);
            return arrowEntity;
        }
        #endregion
    }
}
