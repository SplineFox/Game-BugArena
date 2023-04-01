using UnityEngine;
using System;
using System.Collections.Generic;

namespace SingleUseWorld
{
    public class EnemySpawner : ITickable
    {
        #region Nested Classes
        [Serializable]
        public class Settings
        {
            public float SpawnDistance = 5f;
        }
        #endregion

        #region Fields
        private Settings _settings;
        private LevelBoundary _levelBoundary;
        private EnemyPool _enemyPool;
        private Player _player;

        private List<Enemy> _enemies;
        private int _desiredEnemiesAmount;
        #endregion

        #region Properties
        public bool ShouldSpawn { get; set; }
        #endregion

        #region Delegates & Events
        public event Action<Enemy> EnemyDied = delegate { };
        #endregion

        #region Constructors
        public EnemySpawner(Settings settings, LevelBoundary levelBoundary, EnemyPool enemyPool, Player player)
        {
            _settings = settings;
            _levelBoundary = levelBoundary;
            _enemyPool = enemyPool;
            _player = player;

            _enemies = new List<Enemy>();
            _desiredEnemiesAmount = 0;
        }
        #endregion

        #region Public Methods
        public void Tick(float deltaTime)
        {
            if(ShouldSpawn)
                SpawnEnemies();
        }

        public void SetDesiredAmount(int desiredEnemiesAmount)
        {
            _desiredEnemiesAmount = desiredEnemiesAmount;
        }
        #endregion

        #region Private Methods
        private void SpawnEnemy()
        {
            var enemy = _enemyPool.Get(EnemyType.Wanderer);
            var position = FindPositionForEnemy(enemy);
            enemy.OnSpawned(position, this);
            _enemies.Add(enemy);

        }

        public void DespawnEnemy(Enemy enemy)
        {
            enemy.OnDespawned();
            _enemies.Remove(enemy);
            _enemyPool.Release(enemy);
        }

        public void SpawnEnemies()
        {
            if (_enemies.Count < _desiredEnemiesAmount)
            {
                var enemiesAmountToSpawn = _desiredEnemiesAmount - _enemies.Count;
                for (int index = 0; index < enemiesAmountToSpawn; index++)
                {
                    SpawnEnemy();
                }
            }
        }

        public void DespawnEnemies()
        {
            for (int index = _enemies.Count - 1; index >= 0; index--)
            {
                var enemy = _enemies[index];
                DespawnEnemy(enemy);
            }
        }

        public void OnEnemyDied(Enemy enemy)
        {
            EnemyDied.Invoke(enemy);
        }

        private Vector3 FindPositionForEnemy(Enemy enemy)
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