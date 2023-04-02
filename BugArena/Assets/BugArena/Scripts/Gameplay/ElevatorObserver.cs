using UnityEngine;

namespace BugArena
{
    public abstract class ElevatorObserver : MonoBehaviour
    {
        public abstract void UpdateHeight(float height);
    }
}