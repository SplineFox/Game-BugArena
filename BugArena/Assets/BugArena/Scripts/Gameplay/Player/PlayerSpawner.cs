using System;
using UnityEngine;

namespace BugArena
{
    public class PlayerSpawner
    {
        #region Fields
        private LevelBoundary _levelBoundary;
        private Transform _playerContainer;
        private Player _player;
        #endregion

        #region Delegates & Events
        public event Action PlayerDied = delegate { };
        public event Action PlayerDespawned = delegate { };
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

            _player.Died += PlayerDied.Invoke;
            _player.gameObject.SetActive(true);
            _player.OnSpawned(position, this);
            _player.OnReset();
        }

        public void DespawnPlayer()
        {
            if (!_player.gameObject.activeSelf)
                return;

            _player.OnDespawned();
            _player.gameObject.SetActive(false);
            _player.Died -= PlayerDied.Invoke;

            PlayerDespawned.Invoke();
        }
        #endregion
    }
}