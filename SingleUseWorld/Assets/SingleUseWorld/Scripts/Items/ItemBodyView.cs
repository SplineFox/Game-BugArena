using System.Collections;
using UnityEngine;

namespace SingleUseWorld
{
    public class ItemBodyView : BodyView
    {
        #region Fields
        protected float _rotationSpeed = 4f;
        protected float _rotationAngle = 4f;
        #endregion

        #region Properties
        #endregion

        #region Delegates & Events
        #endregion

        #region Constructors
        #endregion

        #region LifeCycle Methods
        #endregion

        #region Public Methods
        #endregion

        #region Internal Methods
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        private IEnumerator Rotate()
        {
            float rotationProgress = 0f;
            float sinShift = Mathf.PI / 2;

            while (true)
            {
                rotationProgress += _rotationSpeed * Time.deltaTime;
                float rotation = Mathf.Sin(rotationProgress - sinShift) * _rotationAngle;
                transform.localRotation = Quaternion.Euler(0, 0, rotation);
                yield return null;
            }
        }
        #endregion
    }
}