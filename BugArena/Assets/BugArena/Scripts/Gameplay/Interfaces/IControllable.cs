using UnityEngine;

namespace BugArena
{
    public interface IControllable
    {
        void SetMovementDirection(Vector2 direction);

        void StartMovement();

        void StopMovement();

        void SetArmamentDirection(Vector2 direction);

        void Use();

        void Drop();
    }
}