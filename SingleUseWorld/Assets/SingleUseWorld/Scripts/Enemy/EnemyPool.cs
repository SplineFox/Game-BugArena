using UnityEngine;

namespace SingleUseWorld
{
    public class EnemyPool
    {
        #region Fields
        private MonoPool<Enemy> _wandererEnemyPool;
        private MonoPool<Enemy> _chaserEnemyPool;
        private MonoPool<Enemy> _exploderEnemyPool;
        #endregion

        #region Constructors
        public EnemyPool(MonoPool<Enemy> wandererEnemyPool, MonoPool<Enemy> chaserEnemyPool, MonoPool<Enemy> exploderEnemyPool)
        {
            _wandererEnemyPool = wandererEnemyPool;
            _chaserEnemyPool = chaserEnemyPool;
            _exploderEnemyPool = exploderEnemyPool;
        }
        #endregion

        #region Public Methods
        public Enemy Get(EnemyType enemyType)
        {
            Enemy enemy = default;
            switch (enemyType)
            {
                case EnemyType.Wanderer:
                    enemy = _wandererEnemyPool.Get();
                    break;
                case EnemyType.Chaser:
                    enemy = _chaserEnemyPool.Get();
                    break;
                case EnemyType.Exploder:
                    enemy = _exploderEnemyPool.Get();
                    break;
            }

            return enemy;
        }

        public void Release(Enemy enemy)
        {
            switch (enemy.Type)
            {
                case EnemyType.Wanderer:
                    _wandererEnemyPool.Release(enemy);
                    break;
                case EnemyType.Chaser:
                    _chaserEnemyPool.Release(enemy);
                    break;
                case EnemyType.Exploder:
                    _exploderEnemyPool.Release(enemy);
                    break;
            }
        }
        #endregion
    }
}