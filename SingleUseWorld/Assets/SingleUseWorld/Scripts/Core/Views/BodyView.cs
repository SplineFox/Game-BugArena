using UnityEngine;

namespace SingleUseWorld
{
    public class BodyView : ElevatorObserver
    {
        #region Public Methods
        public override void UpdateHeight(float height)
        {
            transform.position = transform.parent.position + transform.parent.up * height;
        }
        #endregion
    }
}