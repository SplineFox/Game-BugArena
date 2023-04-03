using UnityEngine;

namespace BugArena
{
    public class RockItemFactory : IFactory<Item>
    {
        #region Fields
        private readonly IPrefabProvider _prefabProvider;
        private readonly IConfigProvider _configProvider;
        private readonly IFactory<ItemEntity> _entityFactory;
        #endregion

        #region Public Methods
        public RockItemFactory(IPrefabProvider prefabProvider, IConfigProvider configProvider, IFactory<ItemEntity> entityFactory)
        {
            _prefabProvider = prefabProvider;
            _configProvider = configProvider;
            _entityFactory = entityFactory;
        }

        public Item Create()
        {
            var rockItemPrefab = _prefabProvider.Load<Item>(PrefabPath.RockItem);
            var itemSettings = _configProvider.Load<ItemSettings>(ConfigPath.ItemSettings);
            var rockItemSettings = _configProvider.Load<ItemTypeSettings>(ConfigPath.RockItemSettings);

            var rockItem = Object.Instantiate(rockItemPrefab);
            rockItem.OnCreate(itemSettings, rockItemSettings, _entityFactory);
            return rockItem;
        }
        #endregion
    }
}
