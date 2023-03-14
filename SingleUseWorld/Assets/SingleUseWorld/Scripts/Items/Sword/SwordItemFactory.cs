using UnityEngine;

namespace SingleUseWorld
{
    public class SwordItemFactory : IFactory<Item>
    {
        #region Fields
        private Item _swordItemPrefab;
        private ItemSettings _itemSettings;
        private ItemTypeSettings _itemTypeSettings;
        private SwordEntityFactory _swordEntityFactory;
        #endregion

        #region Public Methods
        public Item Create()
        {
            var swordItem = Object.Instantiate(_swordItemPrefab);
            swordItem.OnCreate(_itemSettings, _itemTypeSettings, _swordEntityFactory);
            return swordItem;
        }
        #endregion
    }
}
