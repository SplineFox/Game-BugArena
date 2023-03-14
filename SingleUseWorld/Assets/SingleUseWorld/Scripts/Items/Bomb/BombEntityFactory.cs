using UnityEngine;

namespace SingleUseWorld
{
    public class BombEntityFactory : IFactory<ItemEntity>
    {
        [SerializeField] private BombEntity _bombEntityPrefab;
        [SerializeField] private BombEntitySettings _bombEntitySettings;

        private Score _score;
        private EffectSpawner _effectSpawner;
        private HitTimer _hitTimer;
        private CameraShaker _cameraShaker;

        #region Public Mehods
        public void Inject(Score score, EffectSpawner effectSpawner, HitTimer hitTimer, CameraShaker cameraShaker)
        {
            _score = score;
            _effectSpawner = effectSpawner;
            _hitTimer = hitTimer;
            _cameraShaker = cameraShaker;
        }

        public ItemEntity Create()
        {
            var bombEntity = Object.Instantiate(_bombEntityPrefab);
            bombEntity.OnCreate(_bombEntitySettings, _score, _effectSpawner, _hitTimer, _cameraShaker);
            return bombEntity;
        }
        #endregion
    }
}
