using System;
using UnityEngine;

namespace SingleUseWorld
{
    public class MonoFactory<TPrefab> : ScriptableObject where TPrefab : MonoBehaviour
    {
        #region Fields
        [SerializeField] protected TPrefab _prefab;
        #endregion

        #region Public Methods
        public TPrefab Create()
        {
            var instance = CreateInstance();
            OnCreate(instance);
            return instance;
        }

        public void Destroy(TPrefab instance)
        {
            if (instance == null)
                return;

            OnDestroy(instance);
            DestroyInstance(instance);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Called right after the object is created.
        /// </summary>
        protected virtual void OnCreate(TPrefab instance)
        {
            // Optional
        }

        /// <summary>
        /// Called right before the object is destroyed.
        /// </summary>
        protected virtual void OnDestroy(TPrefab instance)
        {
            // Optional
        }
        #endregion

        #region Private Methods
        private TPrefab CreateInstance()
        {
            TPrefab instance = Instantiate(_prefab);
            return instance;
        }

        private void DestroyInstance(TPrefab instance)
        {
            Destroy(instance.gameObject);
        }
        #endregion
    }
}
