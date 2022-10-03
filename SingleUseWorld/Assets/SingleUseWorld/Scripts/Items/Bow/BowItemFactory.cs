using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "BowItemFactorySO", menuName = "SingleUseWorld/Factories/Items/BowItem Factory SO")]
    public class BowItemFactory : ScriptableFactory, IMonoFactory<Item>
    {
        #region Fields
        [SerializeField] private Item _bowItemPrefab;
        [SerializeField] private ItemSettings _itemSettings;
        [SerializeField] private ArrowEntityFactory _arrowEntityFactory;
        #endregion

        #region Public Methods
        public Item Create()
        {
            var bowItem = CreateInstance<Item>(_bowItemPrefab);
            bowItem.OnCreate(ItemType.Bow, _arrowEntityFactory, _itemSettings);
            return bowItem;
        }
        #endregion
    }
}
