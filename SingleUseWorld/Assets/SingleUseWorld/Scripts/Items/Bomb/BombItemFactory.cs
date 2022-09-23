using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "BombItemFactorySO", menuName = "SingleUseWorld/Factories/Items/BombItem Factory SO")]
    public class BombItemFactory : ScriptableFactory, IMonoFactory<Item>
    {
        #region Fields
        [SerializeField] private Item _bombItemPrefab;
        [SerializeField] private ItemSettings _itemSettings;
        [SerializeField] private BombEntityFactory _bombEntityFactory;
        #endregion

        #region Public Methods
        public Item Create()
        {
            var bombItem = CreateInstance<Item>(_bombItemPrefab);
            bombItem.OnCreate(ItemType.Bomb, _bombEntityFactory, _itemSettings);
            return bombItem;
        }
        #endregion
    }
}
