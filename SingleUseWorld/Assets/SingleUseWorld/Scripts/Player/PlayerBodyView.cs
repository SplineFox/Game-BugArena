using System;
using System.Collections;
using UnityEngine;

namespace SingleUseWorld
{
    [RequireComponent(typeof(Animator))]
    public class PlayerBodyView : CharacterBodyView
    {
        #region Fields
        private Animator _animator = default;

        [SerializeField] private string _idleUnarmedAnimName = "IdleUnarmed";
        [SerializeField] private string _moveUnarmedAnimName = "MoveUnarmed";
        [SerializeField] private string _idleArmedAnimName = "IdleArmed";
        [SerializeField] private string _moveArmedAnimName = "MoveArmed";
        [SerializeField] private string _knockedAnimName = "Knocked";

        private int _idleUnarmedAnimId = 0;
        private int _moveUnarmedAnimId = 0;
        private int _idleArmedAnimId = 0;
        private int _moveArmedAnimId = 0;
        private int _knockedAnimId = 0;

        private bool _isShaking = false;
        private float _shakeOffset = 0.15f;
        private float _shakeIntensity = 0f;

        private EffectSpawner _effectSpawner;
        #endregion

        #region Delegates & Events
        public Action ThrowStartFrameReached = delegate { };
        public Action ThrowEndFrameReached = delegate { };
        #endregion

        #region LifeCycle Methods
        protected override void Awake()
        {
            base.Awake();

            _animator = GetComponent<Animator>();
            CacheAnimatorParameters();
        }

        protected override void Update()
        {
            base.Update();
            UpdateShake();
        }
        #endregion

        #region Public Methods
        public void Initialize(EffectSpawner effectSpawner)
        {
            _effectSpawner = effectSpawner;
        }
        
        public void PlayIdleUnarmedAnimation()
        {
            bool shouldSync = _animator.CurrentStateIs(_idleArmedAnimId);
            _animator.Play(_idleUnarmedAnimId, shouldSync);
            ResetRoll();
        }

        public void PlayIdleArmedAnimation()
        {
            bool shouldSync = _animator.CurrentStateIs(_idleUnarmedAnimId);
            _animator.Play(_idleArmedAnimId, shouldSync);
            ResetRoll();
        }

        public void PlayMoveUnarmedAnimation()
        {
            bool shouldSync = _animator.CurrentStateIs(_moveArmedAnimId);
            _animator.Play(_moveUnarmedAnimId, shouldSync);
        }

        public void PlayMoveArmedAnimation()
        {
            bool shouldSync = _animator.CurrentStateIs(_moveUnarmedAnimId);
            _animator.Play(_moveArmedAnimId, shouldSync);
        }

        public void PlayKnockedAnimation()
        {
            _animator.Play(_knockedAnimId);
            ResetRoll();
        }

        public void SetShakeIntensity(float shakeIntensity)
        {
            _shakeIntensity = shakeIntensity;
            _isShaking = _shakeIntensity > 0;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Called externally by the animation event.
        /// </summary>
        private void OnStepFrame(AnimationEvent animationEvent)
        {
            _effectSpawner.SpawnEffect(EffectType.StepDust, transform.position);
        }

        private void UpdateShake()
        {
            if (!_isShaking)
                return;

            float shakeMagnitude = _shakeOffset * _shakeIntensity;
            float x = UnityEngine.Random.Range(-1f, 1f) * shakeMagnitude;
            float y = UnityEngine.Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = new Vector3(x, y, 0f);
        }

        private void CacheAnimatorParameters()
        {
            _idleUnarmedAnimId = Animator.StringToHash(_idleUnarmedAnimName);
            _moveUnarmedAnimId = Animator.StringToHash(_moveUnarmedAnimName);
            _idleArmedAnimId = Animator.StringToHash(_idleArmedAnimName);
            _moveArmedAnimId = Animator.StringToHash(_moveArmedAnimName);
            _knockedAnimId = Animator.StringToHash(_knockedAnimName);
        }
        #endregion
    }
}