using UnityEngine;

namespace SingleUseWorld
{
    public class SkullEntityFactory : IFactory<ItemEntity>
    {
        private IPrefabProvider _prefabProvider;
        private IConfigProvider _configProvider;

        private Score _score;
        private HitTimer _hitTimer;
        private CameraShaker _cameraShaker;

        public SkullEntityFactory(IPrefabProvider prefabProvider, IConfigProvider configProvider,
            Score score, HitTimer hitTimer, CameraShaker cameraShaker)
        {
            _prefabProvider = prefabProvider;
            _configProvider = configProvider;

            _score = score;
            _hitTimer = hitTimer;
            _cameraShaker = cameraShaker;
        }

        #region Public Mehods
        public ItemEntity Create()
        {
            var skullEntityPrefab = _prefabProvider.Load<SkullEntity>(PrefabPath.SkullEntity);
            var skullEntitySettings = _configProvider.Load<SkullEntitySettings>(ConfigPath.SkullEntitySettings);

            var skullEntity = Object.Instantiate(skullEntityPrefab);
            skullEntity.OnCreate(skullEntitySettings, _score, _hitTimer, _cameraShaker);
            return skullEntity;
        }
        #endregion
    }
}
