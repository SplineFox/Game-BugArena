using System;
using UnityEngine;

namespace SingleUseWorld
{
    public class Difficulty
    {
        #region Nested Classes
        [Serializable]
        public class Settings
        {
            public int PointsPerEnemy = 25;
            public int InitialEnemiesAmount = 5;
            public int MaximumEnemiesAmount = 10;

            public int PointsPerItem = 25;
            public int InitialItemsAmount = 5;
            public int MaximumItemsAmount = 10;
        }
        #endregion

        #region Fields
        private readonly Settings _settings;
        private readonly Score _score;
        private readonly EnemySpawner _enemySpawner;
        private readonly ItemSpawner _itemSpawner;
        #endregion

        #region Constructors
        public Difficulty(Settings settings, Score score, EnemySpawner enemySpawner, ItemSpawner itemSpawner)
        {
            _settings = settings;
            _score = score;
            _enemySpawner = enemySpawner;
            _itemSpawner = itemSpawner;

            _score.Changed += UpdateDifficulty;
            UpdateDifficulty();
        }
        #endregion

        #region Private Methods
        private void UpdateDifficulty()
        {
            UpdateEnemiesSpawnAmount();
            UpdateItemsSpawnAmount();
        }

        private void UpdateEnemiesSpawnAmount()
        {
            var desiredEnemiesAmount = _settings.InitialEnemiesAmount + Math.Min(_settings.MaximumEnemiesAmount, Mathf.FloorToInt(_score.Points / _settings.PointsPerEnemy));
            _enemySpawner.SetDesiredAmount(desiredEnemiesAmount);
        }

        private void UpdateItemsSpawnAmount()
        {
            var desiredItemsAmount = _settings.InitialItemsAmount + Math.Min(_settings.MaximumItemsAmount, Mathf.FloorToInt(_score.Points / _settings.PointsPerItem));
            _itemSpawner.SetDesiredAmount(desiredItemsAmount);
        }
        #endregion
    }
}