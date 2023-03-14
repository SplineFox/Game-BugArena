using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SingleUseWorld
{
    public class PlayerInput : IPlayerInput, IDisposable, PlayerInputActions.IGameplayActions
    {
        #region Fields
        private PlayerInputActions _inputActions;
        #endregion

        #region Delegates & Events
        public event Action<Vector2> MouseMovePerformed = delegate { };
        public event Action<Vector2> MovePerformed = delegate { };
        
        public event Action MoveStarted = delegate { };
        public event Action MoveCanceled = delegate { };

        public event Action UsePerformed = delegate { };
        public event Action DropPerformed = delegate { };
        public event Action PausePerformed = delegate { };
        #endregion

        #region Constructors
        public PlayerInput()
        {
            InitializeInputActions();
            EnableGameplayInput();
        }

        public void Dispose()
        {
            DisableGameplayInput();
        }
        #endregion

        #region Public Methods
        public void OnMove(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    MoveStarted.Invoke();
                    break;

                case InputActionPhase.Performed:
                    var value = context.ReadValue<Vector2>();
                    MovePerformed.Invoke(value);
                    break;

                case InputActionPhase.Canceled:
                    MoveCanceled.Invoke();
                    break;
            }
        }

        public void OnMouseMove(InputAction.CallbackContext context)
        {
            if(context.phase == InputActionPhase.Performed)
            {
                var value = context.ReadValue<Vector2>();
                MouseMovePerformed.Invoke(value);
            }
        }

        public void OnUse(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                UsePerformed.Invoke();
            }
        }

        public void OnDrop(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                DropPerformed.Invoke();
            }
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                PausePerformed.Invoke();
            }
        }

        public void EnableGameplayInput()
        {
            _inputActions.Gameplay.Enable();
        }

        public void DisableGameplayInput()
        {
            _inputActions.Gameplay.Disable();
        }
        #endregion

        #region Private Methods
        private void InitializeInputActions()
        {
            if (_inputActions == null)
            {
                _inputActions = new PlayerInputActions();
                _inputActions.Gameplay.SetCallbacks(this);
            }
        }
        #endregion
    }
}