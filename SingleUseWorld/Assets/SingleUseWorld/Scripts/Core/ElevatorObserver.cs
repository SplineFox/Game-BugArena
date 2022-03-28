using UnityEngine;

namespace SingleUseWorld
{
    public abstract class ElevatorObserver : MonoBehaviour
    {
        public abstract void UpdateHeight(float height);
    }
}