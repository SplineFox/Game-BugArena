using System;
using UnityEngine;

namespace BugArena
{
    public enum InputType
    {
        Gameplay,
        Menu,
        None
    }

    public interface IInputService
    {
        event Action<Vector2> MouseMovePerformed;
        event Action<Vector2> MovePerformed;

        event Action MoveStarted;
        event Action MoveCanceled;

        event Action UsePerformed;
        event Action DropPerformed;
        event Action PausePerformed;

        event Action<Vector2> MenuMouseMovePerformed;
        event Action MenuMouseClickPerformed;
        event Action MenuUnPausePerformed;

        void SwitchTo(InputType inputType);
    }
}