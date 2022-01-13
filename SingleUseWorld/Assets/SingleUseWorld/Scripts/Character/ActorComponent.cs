using UnityEngine;

namespace SingleUseWorld
{
    public abstract class ActorComponent : MonoBehaviour
    {
        #region LifeCycle Methods
        public virtual void OnInitialize() { }
        public virtual void OnDeinitialize() { }
        public virtual void OnUpdate(float deltaTime) { }
        public virtual void OnFixedUpdate(float fixedDeltaTime) { }
        #endregion
    }
}