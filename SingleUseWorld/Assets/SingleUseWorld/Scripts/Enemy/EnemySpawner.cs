using UnityEngine;
using System;
using System.Collections.Generic;

namespace SingleUseWorld
{
    public class EnemySpawner
    {
        #region Nested Classes
        [Serializable]
        public class Settings
        {
            public int InitialEnemiesAmount = 5;
            public int MaximumEnemiesAmount = 10;
            public float SpawnDistance = 5f;
        }
        #endregion

        #region Fields
        private Settings _settings;
        private Score _score;
        private LevelBoundary _levelBoundary;
        private EnemyPool _enemyPool;
        private Player _player;

        private List<Enemy> _enemies;
        private int _desiredEnemiesAmount;
        #endregion

        #region Constructors
        public EnemySpawner(Settings settings, Score score, LevelBoundary levelBoundary, EnemyPool enemyPool, Player player)
        {
            _settings = settings;
            _score = score;
            _levelBoundary = levelBoundary;
            _enemyPool = enemyPool;
            _player = player;

            _enemies = new List<Enemy>();
        }
        #endregion

        #region LifeCycle Methods
        public void Tick()
        {
            _desiredEnemiesAmount = _settings.InitialEnemiesAmount + Math.Min(25, Mathf.FloorToInt(_score.Points/150));

            if(_enemies.Count < _desiredEnemiesAmount)
            {
                var enemiesAmountToSpawn = _desiredEnemiesAmount - _enemies.Count;
                for (int index = 0; index < enemiesAmountToSpawn; index++)
                {
                    SpawnEnemy();
                }
            }
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        private void SpawnEnemy()
        {
            var enemy = _enemyPool.Get(EnemyType.Wanderer);
            enemy.transform.position = FindPositionForEnemy(enemy);
            enemy.Died += OnEnemyDied;
            _enemies.Add(enemy);
        }

        private void DespawnEnemy(Enemy enemy)
        {
            enemy.Died -= OnEnemyDied;
            _enemies.Remove(enemy);
            _enemyPool.Release(enemy);
        }

        private void DespawnAllEnemies()
        {
            for (int index = _enemies.Count - 1; index >= 0; index--)
            {
                var enemy = _enemies[index];
                DespawnEnemy(enemy);
            }
        }

        private void OnEnemyDied(Enemy enemy)
        {
            DespawnEnemy(enemy);
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