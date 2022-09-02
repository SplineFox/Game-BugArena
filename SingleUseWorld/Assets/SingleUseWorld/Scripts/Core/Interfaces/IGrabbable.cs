using UnityEngine;

namespace SingleUseWorld
{
    public interface IGrabbable
    {
        void Grab(float grabbingTension);

        void Release(float grabbingTension);
    }
}