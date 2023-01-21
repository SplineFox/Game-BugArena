using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "SkullItemFactorySO", menuName = "SingleUseWorld/Factories/Items/SkullItem Factory SO")]
    public class SkullItemFactory : ScriptableFactory, IMonoFactory<Item>
    {
        #region Fields
        [SerializeField] private Item _skullItemPrefab;
        [SerializeField] private ItemSettings _itemSettings;
        [SerializeField] private ItemTypeSettings _itemTypeSettings;
        [SerializeField] private SkullEntityFactory _skullEntityFactory;
        #endregion

        #region Public Methods
        public Item Create()
        {
            var skullItem = CreateInstance<Item>(_skullItemPrefab);
            skullItem.OnCreate(_itemSettings, _itemTypeSettings, _skullEntityFactory);
            return skullItem;
        }
        #endregion
    }
}
