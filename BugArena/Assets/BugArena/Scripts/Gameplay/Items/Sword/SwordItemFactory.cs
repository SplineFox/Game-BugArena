using UnityEngine;

namespace BugArena
{
    public class SwordItemFactory : IFactory<Item>
    {
        #region Fields
        private readonly IPrefabProvider _prefabProvider;
        private readonly IConfigProvider _configProvider;
        private readonly IFactory<ItemEntity> _entityFactory;
        #endregion

        #region Public Methods
        public SwordItemFactory(IPrefabProvider prefabProvider, IConfigProvider configProvider, IFactory<ItemEntity> entityFactory)
        {
            _prefabProvider = prefabProvider;
            _configProvider = configProvider;
            _entityFactory = entityFactory;
        }

        public Item Create()
        {
            var swordItemPrefab = _prefabProvider.Load<Item>(PrefabPath.SwordItem);
            var itemSettings = _configProvider.Load<ItemSettings>(ConfigPath.ItemSettings);
            var swordItemSettings = _configProvider.Load<ItemTypeSettings>(ConfigPath.SwordItemSettings);

            var swordItem = Object.Instantiate(swordItemPrefab);
            swordItem.OnCreate(itemSettings, swordItemSettings, _entityFactory);
            return swordItem;
        }
        #endregion
    }
}
