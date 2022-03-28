using UnityEngine;

namespace SingleUseWorld
{
    public class BodyView : ElevatorObserver
    {
        #region Public Methods
        public override void UpdateHeight(float height)
        {
            transform.localPosition = Vector3.up * height;
        }
        #endregion
    }
}