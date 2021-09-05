using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace SingleUseWorld
{
    /// <summary>  
    /// Input detection intermediate layer between Unity InputSystem and scripts.
    /// </summary>
    /// <remarks>
    /// Due to the fact that InputReader is ScriptableObject that globally avaliable and scene-independent,
    /// other scripts can access it from any place.
    /// </remarks>
    [CreateAssetMenu(fileName = "InputReaderSO", menuName = "SingleUseWorld/Input/InputReaderSO")]
    public class InputReaderSO : ScriptableObject, GameInputActions.IGameplayActions, GameInputActions.IMenuActions
    {
        #region Fields
        private GameInputActions _inputActions;
        #endregion

        #region Events & Delegates
        // Assign delegate{} to events to initialise them with an empty delegate
        // so we can skip the null check when we use them.

        // Gameplay events.
        public event UnityAction<Vector2> MoveEvent = delegate { };
        public event UnityAction<Vector2> LookEvent = delegate { };
        public event UnityAction UseEvent = delegate { };
        public event UnityAction DropEvent = delegate { };
        public event UnityAction PauseEvent = delegate { };

        // Menu events.
        public event UnityAction MenuMouseMoveEvent = delegate { };
        public event UnityAction MenuConfirmEvent = delegate { };
        public event UnityAction MenuCancelEvent = delegate { };
        public event UnityAction MenuUnpauseEvent = delegate { };
        #endregion

        #region LifeCycle Methods
        private void OnEnable()
        {
            InitializeInputActions();
            EnableGameplayInput();
        }
        private void OnDisable()
        {
            DisableAllInput();
        }
        #endregion

        #region Public Methods
        // Gameplay actions.
        public void OnMove(InputAction.CallbackContext context)
        {
            MoveEvent.Invoke(context.ReadValue<Vector2>());
        }
        public void OnLook(InputAction.CallbackContext context)
        {
            LookEvent.Invoke(context.ReadValue<Vector2>());
        }

        public void OnUse(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                UseEvent.Invoke();
        }

        public void OnDrop(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                DropEvent.Invoke();
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                PauseEvent.Invoke();
        }

        // Menu actions.
        public void OnMouseMove(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                MenuMouseMoveEvent.Invoke();
        }
        public void OnConfirm(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                MenuConfirmEvent.Invoke();
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                MenuCancelEvent.Invoke();
        }

        public void OnUnpause(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                MenuUnpauseEvent.Invoke();
        }

        public void EnableGameplayInput()
        {
            _inputActions.Menu.Disable();
            _inputActions.Gameplay.Enable();
        }

        public void EnableMenuInput()
        {
            _inputActions.Gameplay.Disable();
            _inputActions.Menu.Enable();
        }

        public void DisableAllInput()
        {
            _inputActions.Menu.Disable();
            _inputActions.Gameplay.Disable();
        }
        #endregion

        #region Private Methods
        private void InitializeInputActions()
        {
            if (_inputActions == null)
            {
                _inputActions = new GameInputActions();
                _inputActions.Gameplay.SetCallbacks(this);
                _inputActions.Menu.SetCallbacks(this);
            }
        }
        #endregion
    }
}