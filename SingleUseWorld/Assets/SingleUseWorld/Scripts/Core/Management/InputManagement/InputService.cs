using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SingleUseWorld
{
    public class InputService : IInputService, PlayerInputActions.IGameplayActions, PlayerInputActions.IMenuActions
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

        public event Action<Vector2> MenuMouseMovePerformed = delegate { };
        public event Action MenuMouseClickPerformed = delegate { };
        public event Action MenuUnPausePerformed = delegate { };
        #endregion

        #region Constructors
        public InputService()
        {
            InitializeInputActions();
            SwitchTo(InputType.Gameplay);
        }
        #endregion

        #region Gameplay Actions
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
        #endregion

        #region Menu Actions
        public void OnMenuMousePoint(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                var value = context.ReadValue<Vector2>();
                MenuMouseMovePerformed.Invoke(value);
            }
        }

        public void OnMenuMouseMove(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                var value = context.ReadValue<Vector2>();
                MenuMouseMovePerformed.Invoke(value);
            }
        }

        public void OnMenuMouseClick(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                MenuMouseClickPerformed.Invoke();
            }
        }

        public void OnMenuUnpause(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                MenuUnPausePerformed.Invoke();
            }
        }

        public void SwitchTo(InputType inputType)
        {
            switch (inputType)
            {
                case InputType.Gameplay:
                    _inputActions.Gameplay.Enable();
                    _inputActions.Menu.Disable();
                    break;
                case InputType.Menu:
                    _inputActions.Menu.Enable();
                    _inputActions.Gameplay.Disable();
                    break;
                case InputType.None:
                    _inputActions.Menu.Disable();
                    _inputActions.Gameplay.Disable();
                    break;
            }
        }
        #endregion

        #region Private Methods
        private void InitializeInputActions()
        {
            if (_inputActions == null)
            {
                _inputActions = new PlayerInputActions();
                _inputActions.Gameplay.SetCallbacks(this);
                _inputActions.Menu.SetCallbacks(this);
            }
        }
        #endregion
    }
}