using UnityEngine;

namespace SingleUseWorld
{
    public class PlayerController
    {
        #region Fields
        private PlayerInput _playerInput;
        private IControllable _player;
        #endregion

        #region Public Methods
        public void Initialize(PlayerInput playerInput, IControllable player)
        {
            _playerInput = playerInput;
            _player = player;
            Subscribe();
        }

        public void Deinitialize()
        {
            Unsubscribe();
        }
        #endregion

        #region Private Methods
        private void Subscribe()
        {
            _playerInput.MoveStarted += _player.StartMovement;
            _playerInput.MovePerformed += _player.SetMovementDirection;
            _playerInput.MoveCanceled += _player.StopMovement;

            _playerInput.UsePerformed += _player.Use;
            _playerInput.DropPerformed += _player.Drop;
        }

        private void Unsubscribe()
        {
            _playerInput.MoveStarted -= _player.StartMovement;
            _playerInput.MovePerformed -= _player.SetMovementDirection;
            _playerInput.MoveCanceled -= _player.StopMovement;

            _playerInput.UsePerformed -= _player.Use;
            _playerInput.DropPerformed -= _player.Drop;
        }
        #endregion
    }
}