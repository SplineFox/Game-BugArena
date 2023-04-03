using UnityEngine;

namespace BugArena
{
    public class RockEntityFactory : IFactory<ItemEntity>
    {
        private IPrefabProvider _prefabProvider;
        private IConfigProvider _configProvider;
        private IScoreAccessService _scoreAccessService;
        private IVisualFeedback _visualFeedback;

        public RockEntityFactory(IPrefabProvider prefabProvider, IConfigProvider configProvider, 
            IScoreAccessService scoreAccessService, IVisualFeedback visualFeedback)
        {
            _prefabProvider = prefabProvider;
            _configProvider = configProvider;
            _scoreAccessService = scoreAccessService;
            _visualFeedback = visualFeedback;
        }

        #region Public Mehods
        public ItemEntity Create()
        {
            var rockEntityPrefab = _prefabProvider.Load<RockEntity>(PrefabPath.RockEntity);
            var rockEntitySettings = _configProvider.Load<RockEntitySettings>(ConfigPath.RockEntitySettings);

            var rockEntity = Object.Instantiate(rockEntityPrefab);
            rockEntity.OnCreate(rockEntitySettings, _scoreAccessService.Score, _visualFeedback.Timer, _visualFeedback.Shaker);
            return rockEntity;
        }
        #endregion
    }
}
