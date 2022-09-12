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
            OnAfterCreate(instance);
            return instance;
        }
        #endregion

        #region Protected
        /// <summary>
        /// Called right after the object is created.
        /// </summary>
        protected virtual void OnAfterCreate(TPrefab instance)
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
        #endregion
    }
}
