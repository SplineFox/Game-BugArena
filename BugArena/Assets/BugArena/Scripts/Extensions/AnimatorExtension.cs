namespace UnityEngine
{
    public static class AnimatorExtension
    {
        public static void Play(this Animator animator, string stateName, bool syncWithCurrent = false)
        {
            float normalizedTime = syncWithCurrent ? animator.GetCurrentNormalizedTime() : 0f;
            animator.Play(stateName, 0, normalizedTime);
        }

        public static void Play(this Animator animator, int stateNameHash, bool syncWithCurrent = false)
        {
            float normalizedTime = syncWithCurrent ? animator.GetCurrentNormalizedTime() : 0f;
            animator.Play(stateNameHash, 0, normalizedTime);
        }

        public static float GetCurrentNormalizedTime(this Animator animator)
        {
            return animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }

        public static bool CurrentStateIs(this Animator animator, string stateName)
        {
            return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
        }

        public static bool CurrentStateIs(this Animator animator, int stateNameHash)
        {
            return animator.GetCurrentAnimatorStateInfo(0).shortNameHash == stateNameHash;
        }
    }
}