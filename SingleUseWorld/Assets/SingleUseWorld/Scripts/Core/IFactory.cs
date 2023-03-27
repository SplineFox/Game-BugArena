using UnityEngine;

namespace SingleUseWorld
{
    public interface IFactory<TBehavior> where TBehavior : MonoBehaviour
    {
        public TBehavior Create();
    }
}
