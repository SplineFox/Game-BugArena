using UnityEngine;

namespace SingleUseWorld
{
    [RequireComponent(typeof(Animator))]
    public class Effect : MonoBehaviour, IPoolable
    {
        #region Fields
        private Animator _animator;
        private EffectSpawner _spawner;
        private EffectType _effectType;
        #endregion

        #region Properties
        public EffectType Type 
        {
            get => _effectType;
        }
        #endregion

        #region LifeCycle Methods
        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        #endregion

        #region Public Methods
        public void OnCreate(EffectType effectType)
        {
            _effectType = effectType;
        }

        public void OnDestroy()
        { }

        void IPoolable.OnReset()
        {
        }

        public void OnSpawned(Vector3 position, EffectSpawner spawner)
        {
            transform.position = position;
            _spawner = spawner;
        }

        public void OnDespawned()
        {
            _spawner = null;
        }
        #endregion

        #region Private Methods
        private void OnAnimationEndFrame()
        {
            _spawner.DespawnEffect(this);
        }
        #endregion
    }
}