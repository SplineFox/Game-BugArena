using UnityEngine;

namespace BugArena
{
    public sealed class EnemyFactory : IFactory<Enemy>
    {
        #region Fields
        private readonly IPrefabProvider _prefabProvider;
        private readonly IConfigProvider _configProvider;
        private readonly IEffectSpawner _effectSpawner;
        #endregion

        #region Public Methods
        public EnemyFactory(IPrefabProvider prefabProvider, IConfigProvider configProvider, IEffectSpawner effectSpawner)
        {
            _prefabProvider = prefabProvider;
            _configProvider = configProvider;
            _effectSpawner = effectSpawner;
        }

        public Enemy Create()
        {
            var enemyPrefab = _prefabProvider.Load<Enemy>(PrefabPath.Enemy);
            var enemySettings = _configProvider.Load<EnemySettings>(ConfigPath.EnemySettings);

            var enemy = Object.Instantiate(enemyPrefab);
            enemy.OnCreate(enemySettings, _effectSpawner);
            return enemy;
        }
        #endregion
    }
}