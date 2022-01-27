using UnityEngine;

namespace SingleUseWorld
{
    public class BodySubview : MonoBehaviour
    {
        #region Public Methods
        public void UpdateHeightPresentation(float height)
        {
            transform.localPosition = Vector3.up * height;
        }
        #endregion
    }
}