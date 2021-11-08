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
        [SerializeField] string _idleAnimationName      = "Idle";
        [SerializeField] string _idleCarryAnimationName = "IdleCarry";
        [SerializeField] string _moveAnimationName      = "Move";
        [SerializeField] string _moveCarryAnimationName = "MoveCarry";
        [SerializeField] string _throwAnimationName     = "Throw";
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
            Play(_idleAnimationName, CurrentNameIs(_idleCarryAnimationName));
        }

        public void PlayIdleCarry()
        {
            Play(_idleCarryAnimationName, CurrentNameIs(_idleAnimationName));
        }

        public void PlayMove()
        {
            Play(_moveAnimationName, CurrentNameIs(_moveCarryAnimationName));
        }

        public void PlayMoveCarry()
        {
            Play(_moveCarryAnimationName, CurrentNameIs(_moveAnimationName));
        }

        public void PlayThrow()
        {
            _animator.Play(_throwAnimationName);
        }
        #endregion

        #region Private Methods
        private void CacheComponents()
        {
            var character = GetComponent<Character>();
            Assert.IsNotNull(character, "\"Character\" is required.");

            _animator = character.View.GetComponent<Animator>();
            Assert.IsNotNull(_animator, "\"Animator\" is required.");
        }

        private void Play(string stateName, bool syncWithCurrent = false)
        {
            float normalizedTime = syncWithCurrent ? CurrentNormalizedTime() : 0f;
            _animator.Play(stateName, _animatorLayer, normalizedTime);
        }

        private bool CurrentNameIs(string stateName)
        {
            return _animator.GetCurrentAnimatorStateInfo(_animatorLayer).IsName(stateName);
        }

        private float CurrentNormalizedTime()
        {
            return _animator.GetCurrentAnimatorStateInfo(_animatorLayer).normalizedTime;
        }
        #endregion
    }
}