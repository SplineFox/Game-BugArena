using UnityEngine;

namespace BugArena
{
    public class SkullItemFactory : IFactory<Item>
    {
        #region Fields
        private readonly IPrefabProvider _prefabProvider;
        private readonly IConfigProvider _configProvider;
        private readonly IFactory<ItemEntity> _entityFactory;
        #endregion

        #region Public Methods
        public SkullItemFactory(IPrefabProvider prefabProvider, IConfigProvider configProvider, IFactory<ItemEntity> entityFactory)
        {
            _prefabProvider = prefabProvider;
            _configProvider = configProvider;
            _entityFactory = entityFactory;
        }

        public Item Create()
        {
            var skullItemPrefab = _prefabProvider.Load<Item>(PrefabPath.SkullItem);
            var itemSettings = _configProvider.Load<ItemSettings>(ConfigPath.ItemSettings);
            var skullItemSettings = _configProvider.Load<ItemTypeSettings>(ConfigPath.SkullItemSettings);

            var skullItem = Object.Instantiate(skullItemPrefab);
            skullItem.OnCreate(itemSettings, skullItemSettings, _entityFactory);
            return skullItem;
        }
        #endregion
    }
}
