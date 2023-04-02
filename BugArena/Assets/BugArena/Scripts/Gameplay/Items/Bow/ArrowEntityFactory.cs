using UnityEngine;

namespace BugArena
{
    public class ArrowEntityFactory : IFactory<ItemEntity>
    {
        private IPrefabProvider _prefabProvider;
        private IConfigProvider _configProvider;
        private IScoreAccessService _scoreAccessService;
        private IVisualFeedback _visualFeedback;

        public ArrowEntityFactory(IPrefabProvider prefabProvider, IConfigProvider configProvider, IScoreAccessService scoreAccessService,
            IVisualFeedback visualFeedback)
        {
            _prefabProvider = prefabProvider;
            _configProvider = configProvider;
            _scoreAccessService = scoreAccessService;
            _visualFeedback = visualFeedback;
        }

        #region Public Mehods
        public ItemEntity Create()
        {
            var arrowEntityPrefab = _prefabProvider.Load<ArrowEntity>(PrefabPath.ArrowEntity);
            var arrowEntitySettings = _configProvider.Load<ArrowEntitySettings>(ConfigPath.ArrowEntitySettings);

            var arrowEntity = Object.Instantiate(arrowEntityPrefab);
            arrowEntity.OnCreate(arrowEntitySettings, _scoreAccessService.Score, _visualFeedback.Timer, _visualFeedback.Shaker);
            return arrowEntity;
        }
        #endregion
    }
}
