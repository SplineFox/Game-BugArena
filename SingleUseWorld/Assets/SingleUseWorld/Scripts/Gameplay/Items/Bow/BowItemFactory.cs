using UnityEngine;

namespace SingleUseWorld
{
    public class BowItemFactory : IFactory<Item>
    {
        #region Fields
        private readonly IPrefabProvider _prefabProvider;
        private readonly IConfigProvider _configProvider;
        private readonly IFactory<ItemEntity> _entityFactory;
        #endregion

        #region Public Methods
        public BowItemFactory(IPrefabProvider prefabProvider, IConfigProvider configProvider, IFactory<ItemEntity> entityFactory)
        {
            _prefabProvider = prefabProvider;
            _configProvider = configProvider;
            _entityFactory = entityFactory;
        }

        public Item Create()
        {
            var bowItemPrefab = _prefabProvider.Load<Item>(PrefabPath.BowItem);
            var itemSettings = _configProvider.Load<ItemSettings>(ConfigPath.ItemSettings);
            var bowItemSettings = _configProvider.Load<ItemTypeSettings>(ConfigPath.BowItemSettings);

            var bowItem = Object.Instantiate(bowItemPrefab);
            bowItem.OnCreate(itemSettings, bowItemSettings, _entityFactory);
            return bowItem;
        }
        #endregion
    }
}
