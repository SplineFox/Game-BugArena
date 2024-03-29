using UnityEngine;

namespace BugArena
{
    public class ItemPool
    {
        #region Fields
        private MonoPool<Item> _rockItemPool;
        private MonoPool<Item> _bowItemPool;
        private MonoPool<Item> _bombItemPool;
        private MonoPool<Item> _swordItemPool;
        #endregion

        #region Constructors
        public ItemPool(MonoPool<Item> rockItemPool, MonoPool<Item> bowItemPool, MonoPool<Item> bombItemPool, MonoPool<Item> swordItemPool)
        {
            _rockItemPool = rockItemPool;
            _bowItemPool = bowItemPool;
            _bombItemPool = bombItemPool;
            _swordItemPool = swordItemPool;
        }
        #endregion

        #region Public Methods
        public Item Get(ItemType itemType)
        {
            Item item = default;
            switch (itemType)
            {
                case ItemType.Rock:
                    item = _rockItemPool.Get();
                    break;
                case ItemType.Bow:
                    item = _bowItemPool.Get();
                    break;
                case ItemType.Sword:
                    item = _swordItemPool.Get();
                    break;
                case ItemType.Bomb:
                    item = _bombItemPool.Get();
                    break;
                default:
                    item = _rockItemPool.Get();
                    break;
            }
            return item;
        }

        public void Release(Item item)
        {
            switch (item.Type)
            {
                case ItemType.Rock:
                    _rockItemPool.Release(item);
                    break;
                case ItemType.Bow:
                    _bowItemPool.Release(item);
                    break;
                case ItemType.Sword:
                    _swordItemPool.Release(item);
                    break;
                case ItemType.Bomb:
                    _bombItemPool.Release(item);
                    break;
            }
        }
        #endregion
    }
}