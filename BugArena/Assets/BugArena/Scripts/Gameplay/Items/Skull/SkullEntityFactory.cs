using UnityEngine;

namespace BugArena
{
    public class SkullEntityFactory : IFactory<ItemEntity>
    {
        private IPrefabProvider _prefabProvider;
        private IConfigProvider _configProvider;
        private IScoreAccessService _scoreAccessService;
        private IVisualFeedback _visualFeedback;

        public SkullEntityFactory(IPrefabProvider prefabProvider, IConfigProvider configProvider, 
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
            var skullEntityPrefab = _prefabProvider.Load<SkullEntity>(PrefabPath.SkullEntity);
            var skullEntitySettings = _configProvider.Load<SkullEntitySettings>(ConfigPath.SkullEntitySettings);

            var skullEntity = Object.Instantiate(skullEntityPrefab);
            skullEntity.OnCreate(skullEntitySettings, _scoreAccessService.Score, _visualFeedback.Timer, _visualFeedback.Shaker);
            return skullEntity;
        }
        #endregion
    }
}
