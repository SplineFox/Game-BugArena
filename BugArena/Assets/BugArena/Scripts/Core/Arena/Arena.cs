using System;
using UnityEngine;

namespace BugArena
{
    public class Arena
    {
        #region Fields
        private readonly PlayerSpawner _playerSpawner;
        private readonly EnemySpawner _enemySpawner;
        private readonly ItemSpawner _itemSpawner;
        #endregion

        #region Constructors
        public Arena(PlayerSpawner playerSpawner, EnemySpawner enemySpawner, ItemSpawner itemSpawner)
        {
            _playerSpawner = playerSpawner;
            _enemySpawner = enemySpawner;
            _itemSpawner = itemSpawner;
        }
        #endregion

        #region Public Methods
        public void Populate()
        {
            _playerSpawner.SpawnPlayer();
            _enemySpawner.SpawnEnemies();
            _itemSpawner.SpawnItems();

            _enemySpawner.ShouldSpawn = true;
            _itemSpawner.ShouldSpawn = true;
        }

        public void Depopulate()
        {
            _enemySpawner.ShouldSpawn = false;
            _itemSpawner.ShouldSpawn = false;

            _playerSpawner.DespawnPlayer();
            _enemySpawner.DespawnEnemies();
            _itemSpawner.DespawnItems();
        }
        #endregion
    }
}