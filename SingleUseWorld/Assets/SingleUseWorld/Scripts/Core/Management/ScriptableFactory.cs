using System;
using UnityEngine;

namespace SingleUseWorld
{
    public abstract class ScriptableFactory : ScriptableObject
    {
        #region Protected
        protected TPrefab CreateInstance<TPrefab>(TPrefab prefab) where TPrefab : MonoBehaviour
        {
            TPrefab instance = Instantiate(prefab);
            return instance;
        }
        #endregion
    }
}
