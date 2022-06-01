using System;
using UnityEngine;

namespace SingleUseWorld
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class PlayerBodyView : BodyView
    {
        #region Fields
        private Animator _animator = default;
        private SpriteRenderer _spriteRenderer = default;

        [SerializeField] private string _facingDirectionXParamName = "FacingX";
        [SerializeField] private string _facingDirectionYParamName = "FacingY";

        [SerializeField] private string _idleUnarmedAnimName = "IdleUnarmed";
        [SerializeField] private string _moveUnarmedAnimName = "MoveUnarmed";
        [SerializeField] private string _idleArmedAnimName = "IdleArmed";
        [SerializeField] private string _moveArmedAnimName = "MoveArmed";
        [SerializeField] private string _knockedAnimName = "Knocked";
        [SerializeField] private string _throwAnimName = "Throw";

        private int _facingDirectionXParamId = 0;
        private int _facingDirectionYParamId = 0;

        private int _idleUnarmedAnimId = 0;
        private int _moveUnarmedAnimId = 0;
        private int _idleArmedAnimId = 0;
        private int _moveArmedAnimId = 0;
        private int _knockedAnimId = 0;
        private int _throwAnimId = 0;

        [SerializeField]
        private StepDustView _stepDustView = default;
        #endregion

        #region Delegates & Events
        public Action ThrowStartFrameReached = delegate { };
        public Action ThrowEndFrameReached = delegate { };
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

        public void PlayIdleUnarmedAnimation()
        {
            bool shouldSync = _animator.CurrentStateIs(_idleArmedAnimId);
            _animator.Play(_idleUnarmedAnimId, shouldSync);
        }

        public void PlayMoveUnarmedAnimation()
        {
            bool shouldSync = _animator.CurrentStateIs(_moveArmedAnimId);
            _animator.Play(_moveUnarmedAnimId, shouldSync);
        }

        public void PlayIdleArmedAnimation()
        {
            bool shouldSync = _animator.CurrentStateIs(_idleUnarmedAnimId);
            _animator.Play(_idleArmedAnimId, shouldSync);
        }

        public void PlayMoveArmedAnimation()
        {
            bool shouldSync = _animator.CurrentStateIs(_moveUnarmedAnimId);
            _animator.Play(_moveArmedAnimId, shouldSync);
        }

        public void PlayKnockedAnimation()
        {
            _animator.Play(_knockedAnimId);
        }

        public void PlayThrowAnimation()
        {
            _animator.Play(_throwAnimId);
        }

        /// <summary>
        /// Called externally by the animation event.
        /// </summary>
        private void OnThrowStartFrame()
        {
            ThrowStartFrameReached.Invoke();
        }

        /// <summary>
        /// Called externally by the animation event.
        /// </summary>
        private void OnThrowEndFrame()
        {
            ThrowEndFrameReached.Invoke();
        }

        /// <summary>
        /// Called externally by the animation event.
        /// </summary>
        private void OnStepFrame(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                GameObject.Instantiate(_stepDustView, transform.position, Quaternion.identity);
            }
        }
        #endregion

        #region Private Methods
        private void CacheAnimatorParameters()
        {
            _facingDirectionXParamId = Animator.StringToHash(_facingDirectionXParamName);
            _facingDirectionYParamId = Animator.StringToHash(_facingDirectionYParamName);

            _idleUnarmedAnimId = Animator.StringToHash(_idleUnarmedAnimName);
            _moveUnarmedAnimId = Animator.StringToHash(_moveUnarmedAnimName);
            _idleArmedAnimId = Animator.StringToHash(_idleArmedAnimName);
            _moveArmedAnimId = Animator.StringToHash(_moveArmedAnimName);
            _knockedAnimId = Animator.StringToHash(_knockedAnimName);
            _throwAnimId = Animator.StringToHash(_throwAnimName);
        }
        #endregion
    }
}