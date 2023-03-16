using UnityEngine;

namespace SingleUseWorld
{
    public class ArrowEntityFactory : IFactory<ItemEntity>
    {
        private IPrefabProvider _prefabProvider;
        private IConfigProvider _configProvider;

        private Score _score;
        private HitTimer _hitTimer;
        private CameraShaker _cameraShaker;

        public ArrowEntityFactory(IPrefabProvider prefabProvider, IConfigProvider configProvider,
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
            var arrowEntityPrefab = _prefabProvider.Load<ArrowEntity>(PrefabPath.ArrowEntity);
            var arrowEntitySettings = _configProvider.Load<ArrowEntitySettings>(ConfigPath.ArrowEntitySettings);

            var arrowEntity = Object.Instantiate(arrowEntityPrefab);
            arrowEntity.OnCreate(arrowEntitySettings, _score, _hitTimer, _cameraShaker);
            return arrowEntity;
        }
        #endregion
    }
}
