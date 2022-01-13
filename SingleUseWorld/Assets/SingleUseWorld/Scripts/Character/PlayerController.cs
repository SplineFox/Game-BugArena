using UnityEngine;

namespace SingleUseWorld
{
    public class PlayerController : MonoBehaviour
    {
        #region Fields
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Player _player;
        #endregion

        #region LifeCycle Methods
        private void OnEnable()
        {
            _playerInput.MoveStarted    += _player.StartMovement;
            _playerInput.MovePerformed  += _player.SetMovementDirection;
            _playerInput.MoveCanceled   += _player.StopMovement;
        }

        private void OnDisable()
        {
            _playerInput.MoveStarted    -= _player.StartMovement;
            _playerInput.MovePerformed  -= _player.SetMovementDirection;
            _playerInput.MoveCanceled   -= _player.StopMovement;
        }
        #endregion
    }
}