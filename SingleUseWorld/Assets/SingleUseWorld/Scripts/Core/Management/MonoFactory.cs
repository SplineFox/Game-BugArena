using System;
using UnityEngine;

namespace SingleUseWorld
{
    public abstract class MonoFactory<TPrefab> : ScriptableObject where TPrefab : MonoBehaviour
    {
        #region Fields
        [SerializeField] protected TPrefab _prefab;
        #endregion

        #region Public Methods
        public abstract TPrefab Create();

        public abstract void Destroy(TPrefab instance);
        #endregion

        #region Protected Methods
        protected TPrefab CreateInstance()
        {
            TPrefab instance = Instantiate(_prefab);
            return instance;
        }

        protected void DestroyInstance(TPrefab instance)
        {
            Destroy(instance.gameObject);
        }
        #endregion
    }
}
