using UnityEngine;

namespace SingleUseWorld
{
    public class Arena
    {
        private readonly PlayerSpawner _playerSpawner;
        private readonly EnemySpawner _enemySpawner;
        private readonly ItemSpawner _itemSpawner;

        public Arena(PlayerSpawner playerSpawner, EnemySpawner enemySpawner, ItemSpawner itemSpawner)
        {
            _playerSpawner = playerSpawner;
            _enemySpawner = enemySpawner;
            _itemSpawner = itemSpawner;

            Populate();
        }

        public void Reset()
        {
            Depopulate();
            Populate();
        }

        private void Populate()
        {
            _playerSpawner.SpawnPlayer();
            _enemySpawner.SpawnEnemies();
            _itemSpawner.SpawnItems();
        }

        private void Depopulate()
        {
            _playerSpawner.DespawnPlayer();
            _enemySpawner.DespawnEnemies();
            _itemSpawner.DespawnItems();
        }
    }
}