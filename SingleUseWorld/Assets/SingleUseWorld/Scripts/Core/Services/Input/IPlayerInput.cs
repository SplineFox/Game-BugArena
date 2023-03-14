using System;
using UnityEngine;

namespace SingleUseWorld
{
    internal interface IPlayerInput
    {
        event Action<Vector2> MouseMovePerformed;
        event Action<Vector2> MovePerformed;

        event Action MoveStarted;
        event Action MoveCanceled;

        event Action UsePerformed;
        event Action DropPerformed;
        event Action PausePerformed;

        void EnableGameplayInput();
        void DisableGameplayInput();
    }
}