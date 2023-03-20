using UnityEngine;

namespace SingleUseWorld
{
    public class BombEntityFactory : IFactory<ItemEntity>
    {
        private IPrefabProvider _prefabProvider;
        private IConfigProvider _configProvider;
        private IEffectSpawner _effectSpawner;
        private IScoreAccessService _scoreAccessService;
        private IVisualFeedback _visualFeedback;

        public BombEntityFactory(IPrefabProvider prefabProvider, IConfigProvider configProvider, IEffectSpawner effectSpawner,
            IScoreAccessService scoreAccessService, IVisualFeedback visualFeedback)
        {
            _prefabProvider = prefabProvider;
            _configProvider = configProvider;
            _effectSpawner = effectSpawner;

            _scoreAccessService = scoreAccessService;
            _visualFeedback = visualFeedback;
        }

        #region Public Mehods
        public ItemEntity Create()
        {
            var bombEntityPrefab = _prefabProvider.Load<BombEntity>(PrefabPath.BombItem);
            var bombEntitySettings = _configProvider.Load<BombEntitySettings>(ConfigPath.BombEntitySettings);

            var bombEntity = Object.Instantiate(bombEntityPrefab);
            bombEntity.OnCreate(bombEntitySettings, _scoreAccessService.Score, _effectSpawner, _visualFeedback.Timer, _visualFeedback.Shaker);
            return bombEntity;
        }
        #endregion
    }
}
