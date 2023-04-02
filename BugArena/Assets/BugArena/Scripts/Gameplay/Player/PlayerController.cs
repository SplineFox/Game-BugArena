using UnityEngine;

namespace BugArena
{
    public class PlayerController
    {
        #region Fields
        private IControllable _controllable;

        private IInputService _playerInput;
        private MouseAim _mouseAim;
        private CameraTargeter _cameraTargeter;
        #endregion

        #region Public Methods
        public PlayerController(IInputService playerInput, MouseAim mouseAim, CameraTargeter cameraTargeter)
        {
            _playerInput = playerInput;
            _mouseAim = mouseAim;
            _cameraTargeter = cameraTargeter;
        }

        public void SetPlayer(Player player)
        {
            if (_controllable != null)
            {
                _mouseAim.SetAnchor(null);
                _cameraTargeter.SetAnchor(null);
                Unsubscribe();
                _controllable = null;
            }

            if (player != null)
            {
                _controllable = player;
                Subscribe();
                _mouseAim.SetAnchor(player.transform);
                _cameraTargeter.SetAnchor(player.transform);
            }
        }
        #endregion

        #region Private Methods
        private void Subscribe()
        {
            _playerInput.MouseMovePerformed += this.UpdateAim;

            _playerInput.MoveStarted += _controllable.StartMovement;
            _playerInput.MovePerformed += _controllable.SetMovementDirection;
            _playerInput.MoveCanceled += _controllable.StopMovement;

            _playerInput.UsePerformed += _controllable.Use;
            _playerInput.DropPerformed += _controllable.Drop;
        }

        private void Unsubscribe()
        {
            _playerInput.MouseMovePerformed -= this.UpdateAim;

            _playerInput.MoveStarted -= _controllable.StartMovement;
            _playerInput.MovePerformed -= _controllable.SetMovementDirection;
            _playerInput.MoveCanceled -= _controllable.StopMovement;

            _playerInput.UsePerformed -= _controllable.Use;
            _playerInput.DropPerformed -= _controllable.Drop;
        }

        private void UpdateAim(Vector2 mouseScreenPosition)
        {
            _mouseAim.Update(mouseScreenPosition);
            _cameraTargeter.SetPosition(_mouseAim.AimValue);
            _controllable.SetArmamentDirection(_mouseAim.AimDirection);
        }
        #endregion
    }
}