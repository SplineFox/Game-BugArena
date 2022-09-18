using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "SwordItemFactorySO", menuName = "SingleUseWorld/Factories/Items/SwordItem Factory SO")]
    public class SwordItemFactory : ScriptableFactory, IMonoFactory<Item>
    {
        #region Fields
        [SerializeField] private Item _swordItemPrefab;
        [SerializeField] private SwordEntityFactory _swordEntityFactory;
        #endregion

        #region Public Methods
        public Item Create()
        {
            var swordItem = CreateInstance<Item>(_swordItemPrefab);
            swordItem.OnCreate(ItemType.Sword, _swordEntityFactory);
            return swordItem;
        }
        #endregion
    }
}
