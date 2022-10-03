using UnityEngine;

namespace SingleUseWorld
{
    public interface IGrabbable
    {
        void Grab(float grabbingSlowDown, float grabbingDamagePerSecond);

        void Release(float grabbingSlowDown, float grabbingDamagePerSecond);
    }
}