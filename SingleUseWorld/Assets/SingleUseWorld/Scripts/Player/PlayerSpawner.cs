using UnityEngine;

namespace SingleUseWorld
{
    public class PlayerSpawner
    {
        #region Fields
        private LevelBoundary _levelBoundary;
        private PlayerFactory _playerFactory;
        private PlayerController _playerController;
        private Player _player;
        #endregion

        #region Properties
        public Vector3 PlayerPosition
        {
            get
            {
                return (_player)? _player.transform.position : Vector3.zero;
            }
        }
        #endregion

        #region Constructors
        public PlayerSpawner(LevelBoundary levelBoundary, PlayerFactory playerFactory, PlayerController playerController)
        {
            _levelBoundary = levelBoundary;
            _playerFactory = playerFactory;
            _playerController = playerController;
            _player = null;
        }
        #endregion

        #region Public Methods
        public void SpawnPlayer()
        {
            if (_player)
                return;

            _player = _playerFactory.Create();
            var position = _levelBoundary.GetCenter();
            
            _player.OnSpawned(position, this);
            _playerController.SetPlayer(_player);
        }

        public void DespawnPlayer()
        {
            if (!_player)
                return;

            _player.OnDespawned();
            _playerController.SetPlayer(null);

            GameObject.Destroy(_player.gameObject);
            _player = null;
        }
        #endregion
    }
}