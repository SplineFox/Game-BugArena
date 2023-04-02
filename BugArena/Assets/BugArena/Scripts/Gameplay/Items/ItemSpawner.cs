using UnityEngine;
using System;
using System.Collections.Generic;

namespace BugArena
{
    public class ItemSpawner : ITickable
    {
        #region Nested Classes
        [Serializable]
        public class Settings
        {
            public float SpawnDistance = 2.5f;
            public List<ItemProbability> ItemsProbabilities;
        }

        [Serializable]
        public struct ItemProbability
        {
            public ItemType Type;
            public double Weight;
        }
        #endregion

        #region Fields
        private Settings _settings;
        private LevelBoundary _levelBoundary;
        private ItemPool _itemPool;
        private Player _player;

        private List<Item> _items;
        private int _desiredItemsAmount;
        private WeightedProbability<ItemType> _itemsRoulette;
        #endregion

        #region Properties
        public bool ShouldSpawn { get; set; }
        #endregion

        #region Constructors
        public ItemSpawner(Settings settings, LevelBoundary levelBoundary, ItemPool itemPool, Player player)
        {
            _settings = settings;
            _levelBoundary = levelBoundary;
            _itemPool = itemPool;
            _player = player;

            _items = new List<Item>();
            _itemsRoulette = new WeightedProbability<ItemType>();
            _desiredItemsAmount = 0;

            foreach (var itemProbability in settings.ItemsProbabilities)
            {
                _itemsRoulette.Add(itemProbability.Type, itemProbability.Weight);
            }
        }
        #endregion

        #region Public Methods
        public void Tick(float deltaTime)
        {
            if(ShouldSpawn)
                SpawnItems();
        }

        public void SetDesiredAmount(int desiredItemsAmount)
        {
            _desiredItemsAmount = desiredItemsAmount;
        }
        #endregion

        #region Private Methods
        private void SpawnItem()
        {
            var itemType = _itemsRoulette.Next();
            var item = _itemPool.Get(itemType);
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

        public void SpawnItems()
        {
            if (_items.Count < _desiredItemsAmount)
            {
                var itemsAmountToSpawn = _desiredItemsAmount - _items.Count;
                for (int index = 0; index < itemsAmountToSpawn; index++)
                {
                    SpawnItem();
                }
            }
        }

        public void DespawnItems()
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