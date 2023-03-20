using UnityEngine;

namespace SingleUseWorld
{
    public class PlayerSpawner
    {
        #region Fields
        private LevelBoundary _levelBoundary;
        private PlayerFactory _playerFactory;
        private Transform _playerContainer;
        private Player _player;
        #endregion

        #region Constructors
        public PlayerSpawner(LevelBoundary levelBoundary, Transform playerContainer, PlayerFactory playerFactory)
        {
            _levelBoundary = levelBoundary;
            _playerFactory = playerFactory;
            _playerContainer = playerContainer;
            _player = null;
        }
        #endregion

        #region Public Methods
        public Player SpawnPlayer()
        {
            if (_player)
                return null;

            _player = _playerFactory.Create();
            _player.transform.parent = _playerContainer;
            var position = _levelBoundary.GetCenter();
            
            _player.OnSpawned(position, this);
            return _player;
        }

        public void DespawnPlayer()
        {
            if (!_player)
                return;

            _player.OnDespawned();

            GameObject.Destroy(_player.gameObject);
            _player = null;
        }
        #endregion
    }
}