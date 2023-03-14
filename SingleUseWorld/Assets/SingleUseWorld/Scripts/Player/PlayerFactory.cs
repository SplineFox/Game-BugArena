using UnityEngine;

namespace SingleUseWorld
{
    public class PlayerFactory : IFactory<Player>
    {
        #region Fields
        private readonly IPrefabProvider _prefabProvider;
        private readonly IConfigProvider _configProvider;
        private readonly EffectSpawner _effectSpawner;
        #endregion

        #region Public Methods
        public PlayerFactory(IPrefabProvider prefabProvider, IConfigProvider configProvider, EffectSpawner effectSpawner)
        {
            _prefabProvider = prefabProvider;
            _configProvider = configProvider;
            _effectSpawner = effectSpawner;
        }

        public Player Create()
        {
            var playerPrefab = _prefabProvider.Load<Player>(PrefabPath.Player);
            var playerSettings = _configProvider.Load<PlayerSettings>(ConfigPath.PlayerSettings);
            
            var player = Object.Instantiate(playerPrefab);
            player.OnCreate(playerSettings, _effectSpawner);
            
            return player;
        }
        #endregion
    }
}
