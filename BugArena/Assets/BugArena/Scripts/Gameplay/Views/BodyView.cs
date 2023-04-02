using UnityEngine;

namespace BugArena
{
    public class BodyView : ElevatorObserver
    {
        #region Public Methods
        public override void UpdateHeight(float height)
        {
            transform.position = transform.parent.position + Vector3.up * height;
        }
        #endregion
    }
}