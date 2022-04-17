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

        [SerializeField] private string _moveDirectionXParamName = "MoveX";
        [SerializeField] private string _moveDirectionYParamName = "MoveY";

        [SerializeField] private string _aimDirectionXParamName = "AimX";
        [SerializeField] private string _aimDirectionYParamName = "AimY";

        [SerializeField] private string _idleUnarmedAnimName = "IdleUnarmed";
        [SerializeField] private string _moveUnarmedAnimName = "MoveUnarmed";
        [SerializeField] private string _idleArmedAnimName = "IdleArmed";
        [SerializeField] private string _moveArmedAnimName = "MoveArmed";
        [SerializeField] private string _knockedAnimName = "Knocked";
        [SerializeField] private string _throwAnimName = "Throw";

        private int _moveDirectionXParamId = 0;
        private int _moveDirectionYParamId = 0;

        private int _aimDirectionXParamId = 0;
        private int _aimDirectionYParamId = 0;

        private int _idleUnarmedAnimId = 0;
        private int _moveUnarmedAnimId = 0;
        private int _idleArmedAnimId = 0;
        private int _moveArmedAnimId = 0;
        private int _knockedAnimId = 0;
        private int _throwAnimId = 0;
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
        public void SetMoveDirectionParameter(Vector2 moveDirection)
        {
            _animator.SetFloat(_moveDirectionXParamId, moveDirection.x);
            _animator.SetFloat(_moveDirectionYParamId, moveDirection.y);
            _spriteRenderer.flipX = moveDirection.x < 0;
        }

        public void SetAimDirectionParameter(Vector2 aimDirection)
        {
            _animator.SetFloat(_aimDirectionXParamId, aimDirection.x);
            _animator.SetFloat(_aimDirectionYParamId, aimDirection.y);
            _spriteRenderer.flipX = aimDirection.x < 0;
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
        #endregion

        #region Private Methods
        private void CacheAnimatorParameters()
        {
            _moveDirectionXParamId = Animator.StringToHash(_moveDirectionXParamName);
            _moveDirectionYParamId = Animator.StringToHash(_moveDirectionYParamName);

            _aimDirectionXParamId = Animator.StringToHash(_aimDirectionXParamName);
            _aimDirectionYParamId = Animator.StringToHash(_aimDirectionYParamName);

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