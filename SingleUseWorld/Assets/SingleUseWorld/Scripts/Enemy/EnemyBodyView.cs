using System;
using UnityEngine;

namespace SingleUseWorld
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class EnemyBodyView : BodyView
    {
        #region Fields
        private Animator _animator = default;
        private SpriteRenderer _spriteRenderer = default;

        [SerializeField] private string _facingDirectionXParamName = "FacingX";
        [SerializeField] private string _facingDirectionYParamName = "FacingY";

        [SerializeField] private string _idleAnimName = "Idle";
        [SerializeField] private string _wanderAnimName = "Wander";
        [SerializeField] private string _chaseAnimName = "Chase";
        [SerializeField] private string _knockedAnimName = "Knocked";

        private int _facingDirectionXParamId = 0;
        private int _facingDirectionYParamId = 0;

        private int _idleAnimId = 0;
        private int _wanderAnimId = 0;
        private int _chaseAnimId = 0;
        private int _knockedAnimId = 0;
        #endregion

        #region LifeCycle Methods
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            CacheAnimatorParameters();
        }
        #endregion

        #region Public Methods
        public void SetFacingDirectionParameter(Vector2 facingDirection)
        {
            _animator.SetFloat(_facingDirectionXParamId, facingDirection.x);
            _animator.SetFloat(_facingDirectionYParamId, facingDirection.y);
            _spriteRenderer.flipX = facingDirection.x < 0;
        }

        public void PlayIdleAnimation()
        {
            _animator.Play(_idleAnimId);
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
        }
        #endregion

        #region Private Methods
        private void CacheAnimatorParameters()
        {
            _facingDirectionXParamId = Animator.StringToHash(_facingDirectionXParamName);
            _facingDirectionYParamId = Animator.StringToHash(_facingDirectionYParamName);

            _idleAnimId = Animator.StringToHash(_idleAnimName);
            _wanderAnimId = Animator.StringToHash(_wanderAnimName);
            _chaseAnimId = Animator.StringToHash(_chaseAnimName);
            _knockedAnimId = Animator.StringToHash(_knockedAnimName);
        }
        #endregion
    }
}