using UnityEngine;

namespace SingleUseWorld
{
    public class PlayerView : MonoBehaviour
    {
        #region Fields
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;

        [SerializeField] private string _directionXParamName = "MoveX";
        [SerializeField] private string _directionYParamName = "MoveY";

        [SerializeField] private string _idleAnimName = "Idle";
        [SerializeField] private string _moveAnimName = "Move";
        [SerializeField] private string _idleCarryAnimName = "IdleCarry";
        [SerializeField] private string _moveCarryAnimName = "MoveCarry";
        [SerializeField] private string _throwAnimName = "Throw";

        private int _directionXParamId = 0;
        private int _directionYParamId = 0;

        private int _idleAnimId = 0;
        private int _moveAnimId = 0;
        private int _idleCarryAnimId = 0;
        private int _moveCarryAnimId = 0;
        private int _throwAnimId = 0;
        #endregion

        #region LifeCycle Methods
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _directionXParamId = Animator.StringToHash(_directionXParamName);
            _directionYParamId = Animator.StringToHash(_directionYParamName);

            _idleAnimId = Animator.StringToHash(_idleAnimName);
            _moveAnimId = Animator.StringToHash(_moveAnimName);
            _idleCarryAnimId = Animator.StringToHash(_idleCarryAnimName);
            _moveCarryAnimId = Animator.StringToHash(_moveCarryAnimName);
            _throwAnimId = Animator.StringToHash(_throwAnimName);
        }
        #endregion

        #region Public Methods
        public void SetDirectionParameter(Vector2 direction)
        {
            _animator.SetFloat(_directionXParamId, direction.x);
            _animator.SetFloat(_directionYParamId, direction.y);
            _spriteRenderer.flipX = direction.x < 0;
        }

        public void PlayIdleAnimation()
        {
            bool shouldSync = _animator.CurrentStateIs(_idleCarryAnimId);
            _animator.Play(_idleAnimId, shouldSync);
        }

        public void PlayMoveAnimation()
        {
            bool shouldSync = _animator.CurrentStateIs(_moveCarryAnimId);
            _animator.Play(_moveAnimId, shouldSync);
        }

        public void PlayIdleCarryAnimation()
        {
            bool shouldSync = _animator.CurrentStateIs(_idleAnimId);
            _animator.Play(_idleCarryAnimId, shouldSync);
        }

        public void PlayMoveCarryAnimation()
        {
            bool shouldSync = _animator.CurrentStateIs(_moveAnimId);
            _animator.Play(_moveCarryAnimId, shouldSync);
        }

        public void PlayThrowAnimation()
        {
            _animator.Play(_throwAnimId);
        }
        #endregion
    }
}