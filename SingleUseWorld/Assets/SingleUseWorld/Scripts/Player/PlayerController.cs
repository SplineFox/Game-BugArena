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
        public void Initialize(PlayerInput playerInput, Player player,
            CameraController cameraController, TargetController targetController)
        {
            _playerInput = playerInput;
            _player = player;

            _cameraController = cameraController;
            _targetController = targetController;

            _mouseAim = new MouseAim(_cameraController.Camera, player.transform);

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