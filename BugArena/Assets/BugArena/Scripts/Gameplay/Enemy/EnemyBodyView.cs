using System;
using System.Collections;
using UnityEngine;

namespace BugArena
{
    [RequireComponent(typeof(Animator))]
    public class EnemyBodyView : CharacterBodyView
    {
        #region Fields
        private Animator _animator = default;

        [SerializeField] private string _idleAnimName = "Idle";
        [SerializeField] private string _wanderAnimName = "Wander";
        [SerializeField] private string _chaseAnimName = "Chase";
        [SerializeField] private string _knockedAnimName = "Knocked";

        private int _idleAnimId = 0;
        private int _wanderAnimId = 0;
        private int _chaseAnimId = 0;
        private int _knockedAnimId = 0;
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
        public void PlayIdleAnimation()
        {
            _animator.Play(_idleAnimId);
            ResetRoll();
        }

        public void PlayWanderAnimation()
        {
            _animator.Play(_wanderAnimId);
        }

        public void PlayChaseAnimation()
        {
            _animator.Play(_chaseAnimId);
        }

        public void PlayKnockedAnimation()
        {
            _animator.Play(_knockedAnimId);
            ResetRoll();
            _rollEnabled = false;
        }
        #endregion

        #region Private Methods
        private void CacheAnimatorParameters()
        {
            _idleAnimId = Animator.StringToHash(_idleAnimName);
            _wanderAnimId = Animator.StringToHash(_wanderAnimName);
            _chaseAnimId = Animator.StringToHash(_chaseAnimName);
            _knockedAnimId = Animator.StringToHash(_knockedAnimName);
        }
        #endregion
    }
}