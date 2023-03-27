using UnityEngine;

namespace SingleUseWorld
{
    public class PlayerSpawner
    {
        #region Fields
        private LevelBoundary _levelBoundary;
        private Transform _playerContainer;
        private Player _player;
        #endregion

        #region Constructors
        public PlayerSpawner(LevelBoundary levelBoundary, Transform playerContainer, Player player)
        {
            _levelBoundary = levelBoundary;
            _playerContainer = playerContainer;
            _player = player;

            _player.transform.SetParent(_playerContainer);
            _player.gameObject.SetActive(false);
        }
        #endregion

        #region Public Methods
        public void SpawnPlayer()
        {
            var position = _levelBoundary.GetCenter();

            _player.gameObject.SetActive(true);
            _player.OnSpawned(position, this);
        }

        public void DespawnPlayer()
        {
            _player.OnDespawned();
            _player.gameObject.SetActive(false);
        }
        #endregion
    }
}