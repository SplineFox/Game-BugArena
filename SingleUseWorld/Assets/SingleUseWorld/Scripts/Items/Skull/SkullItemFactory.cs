using UnityEngine;

namespace SingleUseWorld
{
    public class SkullItemFactory : IFactory<Item>
    {
        #region Fields
        private Item _skullItemPrefab;
        private ItemSettings _itemSettings;
        private ItemTypeSettings _itemTypeSettings;
        private SkullEntityFactory _skullEntityFactory;
        #endregion

        #region Public Methods
        public Item Create()
        {
            var skullItem = Object.Instantiate(_skullItemPrefab);
            skullItem.OnCreate(_itemSettings, _itemTypeSettings, _skullEntityFactory);
            return skullItem;
        }
        #endregion
    }
}
