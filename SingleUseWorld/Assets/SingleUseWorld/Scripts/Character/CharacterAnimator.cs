using UnityEngine;
using UnityEngine.Assertions;

namespace SingleUseWorld
{
    /// <summary>
    /// Controls playback of character animations.
    /// </summary>
    /// <remarks>
    /// Methods invoked by the StateMachine StateActions.
    /// </remarks>
    public class CharacterAnimator : MonoBehaviour
    {
        #region Fields
        private Animator _animator = default;

        [SerializeField] int _animatorLayer = 0;
        [SerializeField] string _idleAnimationName      = "Character_Idle";
        [SerializeField] string _moveAnimationName      = "Character_Move";
        [SerializeField] string _idleCarryAnimationName = "Character_IdleCarry";
        [SerializeField] string _moveCarryAnimationName = "Character_MoveCarry";
        [SerializeField] string _throwAnimationName     = "Character_Throw";
        #endregion

        #region LifeCycle Methods
        private void Awake()
        {
            CacheComponents();
        }
        #endregion

        #region Public Methods
        public void PlayIdle()
        {
            PlaySyncWithCurrent(_idleAnimationName);
        }

        public void PlayIdleCarry()
        {
            PlaySyncWithCurrent(_idleCarryAnimationName);
        }

        public void PlayMove()
        {
            PlaySyncWithCurrent(_moveAnimationName);
        }

        public void PlayMoveCarry()
        {
            PlaySyncWithCurrent(_moveCarryAnimationName);
        }

        public void PlayThrow()
        {
            _animator.Play(_throwAnimationName);
        }
        #endregion

        #region Private Methods
        private void CacheComponents()
        {
            _animator = GetComponent<Animator>();
            Assert.IsNotNull(_animator, "\"Animator\" is required.");
        }

        private void PlaySyncWithCurrent(string stateName)
        {
            var currentNormalizedTime = _animator.GetCurrentAnimatorStateInfo(_animatorLayer).normalizedTime;
            _animator.Play(stateName, _animatorLayer, currentNormalizedTime);
        }
        #endregion
    }
}