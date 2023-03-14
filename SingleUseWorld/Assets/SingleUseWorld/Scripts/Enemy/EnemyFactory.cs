using SingleUseWorld.FSM;
using UnityEngine;

namespace SingleUseWorld
{
    public sealed class EnemyFactory : IFactory<Enemy>
    {
        #region Fields
        private Enemy _enemyPrefab;
        private EnemySettings _enemySettings;

        private EffectSpawner _effectSpawner;
        #endregion

        #region Public Methods
        public void Inject(EffectSpawner effectSpawner)
        {
            _effectSpawner = effectSpawner;
        }

        public Enemy Create()
        {
            var enemy = Object.Instantiate(_enemyPrefab);
            enemy.OnCreate(_enemySettings, _effectSpawner);
            return enemy;
        }
        #endregion
    }
}