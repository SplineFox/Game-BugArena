using UnityEngine;

namespace SingleUseWorld
{
    public class PlayerController
    {
        #region Fields
        private PlayerInput _playerInput;
        private IControllable _player;

        private CameraController _cameraController;
        private TargetController _targetController;

        private MouseAim _mouseAim;
        #endregion

        #region Public Methods
        public PlayerController(PlayerInput playerInput, CameraController cameraController, TargetController targetController)
        {
            _playerInput = playerInput;
            _cameraController = cameraController;
            _targetController = targetController;

            _mouseAim = new MouseAim(_cameraController.Camera);
        }

        public void SetPlayer(Player player)
        {
            if (_player != null)
            {
                _mouseAim.SetAnchor(null);
                _targetController.SetAnchor(null);
                Unsubscribe();
                _player = null;
            }

            if (player != null)
            {
                _player = player;
                Subscribe();
                _mouseAim.SetAnchor(player.transform);
                _targetController.SetAnchor(player.transform);
            }
        }
        #endregion

        #region Private Methods
        private void Subscribe()
        {
            _playerInput.MouseMovePerformed += this.UpdateAim;

            _playerInput.MoveStarted += _player.StartMovement;
            _playerInput.MovePerformed += _player.SetMovementDirection;
            _playerInput.MoveCanceled += _player.StopMovement;

            _playerInput.UsePerformed += _player.Use;
            _playerInput.DropPerformed += _player.Drop;
        }

        private void Unsubscribe()
        {
            _playerInput.MouseMovePerformed -= this.UpdateAim;

            _playerInput.MoveStarted -= _player.StartMovement;
            _playerInput.MovePerformed -= _player.SetMovementDirection;
            _playerInput.MoveCanceled -= _player.StopMovement;

            _playerInput.UsePerformed -= _player.Use;
            _playerInput.DropPerformed -= _player.Drop;
        }

        private void UpdateAim(Vector2 mouseScreenPosition)
        {
            _mouseAim.Update(mouseScreenPosition);
            _targetController.SetPosition(_mouseAim.AimValue);
            _player.SetArmamentDirection(_mouseAim.AimDirection);
        }
        #endregion
    }
}