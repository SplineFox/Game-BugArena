using UnityEngine;

namespace SingleUseWorld
{
    public interface IGrabbable
    {
        void GrabbedBy(IGrabber grabInstigator);

        void ReleasedBy(IGrabber grabInstigator);
    }
}