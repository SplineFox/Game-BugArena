using UnityEngine;

namespace SingleUseWorld
{
    public class SwordEntityFactory : IFactory<ItemEntity>
    {
        private IPrefabProvider _prefabProvider;
        private IConfigProvider _configProvider;

        private Score _score;
        private HitTimer _hitTimer;
        private CameraShaker _cameraShaker;

        public SwordEntityFactory(IPrefabProvider prefabProvider, IConfigProvider configProvider,
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
            var swordEntityPrefab = _prefabProvider.Load<SwordEntity>(PrefabPath.SwordEntity);
            var swordEntitySettings = _configProvider.Load<SwordEntitySettings>(ConfigPath.SwordEntitySettings);

            var swordEntity = Object.Instantiate(swordEntityPrefab);
            swordEntity.OnCreate(swordEntitySettings, _score, _hitTimer, _cameraShaker);
            return swordEntity;
        }
        #endregion
    }
}
