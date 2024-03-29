using UnityEngine;

namespace BugArena
{
    [RequireComponent(typeof(Animator))]
    public class Effect : MonoBehaviour
    {
        #region Fields
        private Animator _animator;
        private EffectSpawner _spawner;
        private EffectType _effectType;
        private EffectAppearance _effectAppearance;
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

        protected virtual void Update()
        {
            transform.position = transform.position + (_effectAppearance.velocity * Time.deltaTime);
        }
        #endregion

        #region Public Methods
        public void OnCreate(EffectType effectType)
        {
            _effectType = effectType;
        }

        public void OnDestroy()
        { }

        public void OnSpawned(EffectSpawner spawner, EffectAppearance effectAppearance)
        {
            _spawner = spawner;
            _effectAppearance = effectAppearance;

            transform.position = _effectAppearance.position;
            transform.eulerAngles = _effectAppearance.rotation;

            _animator.ForceStateNormalizedTime(_effectAppearance.playbackStart);
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