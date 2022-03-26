using UnityEngine;

namespace SingleUseWorld
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class PlayerBodyView : BodyView
    {
        #region Fields
        private Animator _animator = default;
        private SpriteRenderer _spriteRenderer = default;

        [SerializeField] private string _directionXParamName = "MoveX";
        [SerializeField] private string _directionYParamName = "MoveY";

        [SerializeField] private string _idleUnarmedAnimName = "IdleUnarmed";
        [SerializeField] private string _moveUnarmedAnimName = "MoveUnarmed";
        [SerializeField] private string _idleArmedAnimName = "IdleArmed";
        [SerializeField] private string _moveArmedAnimName = "MoveArmed";
        [SerializeField] private string _throwAnimName = "Throw";

        private int _directionXParamId = 0;
        private int _directionYParamId = 0;

        private int _idleUnarmedAnimId = 0;
        private int _moveUnarmedAnimId = 0;
        private int _idleArmedAnimId = 0;
        private int _moveArmedAnimId = 0;
        private int _throwAnimId = 0;
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
        public void SetDirectionParameter(Vector2 direction)
        {
            _animator.SetFloat(_directionXParamId, direction.x);
            _animator.SetFloat(_directionYParamId, direction.y);
            _spriteRenderer.flipX = direction.x < 0;
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

        public void PlayThrowAnimation()
        {
            _animator.Play(_throwAnimId);
        }
        #endregion

        #region Private Methods
        private void CacheAnimatorParameters()
        {
            _directionXParamId = Animator.StringToHash(_directionXParamName);
            _directionYParamId = Animator.StringToHash(_directionYParamName);

            _idleUnarmedAnimId = Animator.StringToHash(_idleUnarmedAnimName);
            _moveUnarmedAnimId = Animator.StringToHash(_moveUnarmedAnimName);
            _idleArmedAnimId = Animator.StringToHash(_idleArmedAnimName);
            _moveArmedAnimId = Animator.StringToHash(_moveArmedAnimName);
            _throwAnimId = Animator.StringToHash(_throwAnimName);
        }
        #endregion
    }
}