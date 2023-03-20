using UnityEngine;

namespace SingleUseWorld
{
    public class SwordEntityFactory : IFactory<ItemEntity>
    {
        private IPrefabProvider _prefabProvider;
        private IConfigProvider _configProvider;
        private IScoreAccessService _scoreAccessService;
        private IVisualFeedback _visualFeedback;

        public SwordEntityFactory(IPrefabProvider prefabProvider, IConfigProvider configProvider,
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
            var swordEntityPrefab = _prefabProvider.Load<SwordEntity>(PrefabPath.SwordEntity);
            var swordEntitySettings = _configProvider.Load<SwordEntitySettings>(ConfigPath.SwordEntitySettings);

            var swordEntity = Object.Instantiate(swordEntityPrefab);
            swordEntity.OnCreate(swordEntitySettings, _scoreAccessService.Score, _visualFeedback.Timer, _visualFeedback.Shaker);
            return swordEntity;
        }
        #endregion
    }
}
