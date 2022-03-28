using UnityEngine;

namespace SingleUseWorld
{
    public interface IControllable
    {
        void SetMovementDirection(Vector2 direction);

        void StartMovement();

        void StopMovement();

        void Use();

        void Drop();
    }
}