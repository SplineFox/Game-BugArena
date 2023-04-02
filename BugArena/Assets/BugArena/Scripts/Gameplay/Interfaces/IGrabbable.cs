using UnityEngine;

namespace BugArena
{
    public interface IGrabbable
    {
        void GrabbedBy(IGrabber grabInstigator);

        void ReleasedBy(IGrabber grabInstigator);
    }
}