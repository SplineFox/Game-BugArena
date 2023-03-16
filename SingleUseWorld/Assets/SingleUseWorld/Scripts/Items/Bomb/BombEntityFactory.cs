using UnityEngine;

namespace SingleUseWorld
{
    public class BombEntityFactory : IFactory<ItemEntity>
    {
        private IPrefabProvider _prefabProvider;
        private IConfigProvider _configProvider;
        private IEffectSpawner _effectSpawner;

        private Score _score;
        private HitTimer _hitTimer;
        private CameraShaker _cameraShaker;

        public BombEntityFactory(IPrefabProvider prefabProvider, IConfigProvider configProvider, IEffectSpawner effectSpawner,
            Score score, HitTimer hitTimer, CameraShaker cameraShaker)
        {
            _prefabProvider = prefabProvider;
            _configProvider = configProvider;
            _effectSpawner = effectSpawner;

            _score = score;
            _hitTimer = hitTimer;
            _cameraShaker = cameraShaker;
        }

        #region Public Mehods
        public ItemEntity Create()
        {
            var bombEntityPrefab = _prefabProvider.Load<BombEntity>(PrefabPath.BombItem);
            var bombEntitySettings = _configProvider.Load<BombEntitySettings>(ConfigPath.BombEntitySettings);

            var bombEntity = Object.Instantiate(bombEntityPrefab);
            bombEntity.OnCreate(bombEntitySettings, _score, _effectSpawner, _hitTimer, _cameraShaker);
            return bombEntity;
        }
        #endregion
    }
}
