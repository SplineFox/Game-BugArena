using UnityEngine;

namespace SingleUseWorld
{
    public class BombItemFactory : IFactory<Item>
    {
        #region Fields
        [SerializeField] private Item _bombItemPrefab;
        [SerializeField] private ItemSettings _itemSettings;
        [SerializeField] private ItemTypeSettings _itemTypeSettings;
        [SerializeField] private BombEntityFactory _bombEntityFactory;
        #endregion

        #region Public Methods
        public Item Create()
        {
            var bombItem = Object.Instantiate(_bombItemPrefab);
            bombItem.OnCreate(_itemSettings, _itemTypeSettings, _bombEntityFactory);
            return bombItem;
        }
        #endregion
    }
}
