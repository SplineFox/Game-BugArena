using UnityEngine;

namespace SingleUseWorld
{
    public class BowItemFactory : IFactory<Item>
    {
        #region Fields
        private Item _bowItemPrefab;
        private ItemSettings _itemSettings;
        private ItemTypeSettings _itemTypeSettings;
        private ArrowEntityFactory _arrowEntityFactory;
        #endregion

        #region Public Methods
        public Item Create()
        {
            var bowItem = Object.Instantiate(_bowItemPrefab);
            bowItem.OnCreate(_itemSettings, _itemTypeSettings, _arrowEntityFactory);
            return bowItem;
        }
        #endregion
    }
}
