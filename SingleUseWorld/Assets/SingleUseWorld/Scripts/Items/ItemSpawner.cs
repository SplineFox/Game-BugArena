using UnityEngine;
using System;
using System.Collections.Generic;

namespace SingleUseWorld
{
    public class ItemSpawner
    {
        #region Nested Classes
        [Serializable]
        public class Settings
        {
            public int InitialItemsAmount = 5;
            public int MaximumItemsAmount = 10;
            public float SpawnDistance = 2.5f;
        }
        #endregion

        #region Fields
        private Settings _settings;
        private Score _score;
        private LevelBoundary _levelBoundary;
        private ItemPool _itemPool;
        private Player _player;

        private List<Item> _items;
        private int _desiredItemsAmount;
        #endregion

        #region Constructors
        public ItemSpawner(Settings settings, Score score, LevelBoundary levelBoundary, ItemPool itemPool, Player player)
        {
            _settings = settings;
            _score = score;
            _levelBoundary = levelBoundary;
            _itemPool = itemPool;
            _player = player;

            _items = new List<Item>();
        }
        #endregion

        #region LifeCycle Methods
        public void Tick()
        {
            _desiredItemsAmount = _settings.InitialItemsAmount + Math.Min(25, Mathf.FloorToInt(_score.Points / 150));

            if (_items.Count < _desiredItemsAmount)
            {
                var itemsAmountToSpawn = _desiredItemsAmount - _items.Count;
                for (int index = 0; index < itemsAmountToSpawn; index++)
                {
                    SpawnItem();
                }
            }
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        private void SpawnItem()
        {
            var item = _itemPool.Get(ItemType.Bomb);
            item.transform.position = FindPositionForItem(item);
            item.Used += OnItemUsed;
            _items.Add(item);
        }

        private void DespawnItem(Item item)
        {
            item.Used -= OnItemUsed;
            _items.Remove(item);
            _itemPool.Release(item);
        }

        private void DespawnAllEnemies()
        {
            for (int index = _items.Count - 1; index >= 0; index--)
            {
                var enemy = _items[index];
                DespawnItem(enemy);
            }
        }

        private void OnItemUsed(Item item)
        {
            DespawnItem(item);
        }

        private Vector3 FindPositionForItem(Item item)
        {
            Vector3 positionToSpawn;
            float distanceToPlayer;
            Collider2D collision;

            do
            {
                positionToSpawn = _levelBoundary.GetRandomPositionInside();
                distanceToPlayer = Vector3.Distance(positionToSpawn, _player.transform.position);
                collision = Physics2D.OverlapCircle(positionToSpawn, 1f);
            }
            while (distanceToPlayer < _settings.SpawnDistance && collision != null);

            return positionToSpawn;
        }
        #endregion
    }
}