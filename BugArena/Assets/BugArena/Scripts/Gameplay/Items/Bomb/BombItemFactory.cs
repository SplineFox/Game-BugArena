using UnityEngine;

namespace BugArena
{
    public class BombItemFactory : IFactory<Item>
    {
        #region Fields
        private readonly IPrefabProvider _prefabProvider;
        private readonly IConfigProvider _configProvider;
        private readonly IFactory<ItemEntity> _entityFactory;
        #endregion

        #region Public Methods
        public BombItemFactory(IPrefabProvider prefabProvider, IConfigProvider configProvider, IFactory<ItemEntity> entityFactory)
        {
            _prefabProvider = prefabProvider;
            _configProvider = configProvider;
            _entityFactory = entityFactory;
        }

        public Item Create()
        {
            var bombItemPrefab = _prefabProvider.Load<Item>(PrefabPath.BombItem);
            var itemSettings = _configProvider.Load<ItemSettings>(ConfigPath.ItemSettings);
            var bombItemSettings = _configProvider.Load<ItemTypeSettings>(ConfigPath.BombItemSettings);

            var bombItem = Object.Instantiate(bombItemPrefab);
            bombItem.OnCreate(itemSettings, bombItemSettings, _entityFactory);
            return bombItem;
        }
        #endregion
    }
}
