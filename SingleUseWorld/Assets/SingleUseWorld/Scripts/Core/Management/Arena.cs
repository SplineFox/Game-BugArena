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
        }

        public void Reset()
        {
            _playerSpawner.DespawnPlayer();
            _enemySpawner.DespawnEnemies();
            _itemSpawner.DespawnAllItems();
        }
    }
}