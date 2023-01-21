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
        #endregion

        #region Private Methods
        /// <summary>
        /// Called externally by the animation event.
        /// </summary>
        private void OnStepFrame(AnimationEvent animationEvent)
        {
            _effectSpawner.SpawnEffect(EffectType.StepDust, transform.position);
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