using UnityEngine;

namespace BugArena
{
    public interface IFactory<TBehavior> where TBehavior : MonoBehaviour
    {
        public TBehavior Create();
    }
}
