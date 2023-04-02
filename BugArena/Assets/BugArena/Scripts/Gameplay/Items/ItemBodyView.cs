using System.Collections;
using UnityEngine;

namespace BugArena
{
    public class ItemBodyView : BodyView
    {
        #region Fields
        protected float _rotationSpeed = 4f;
        protected float _rotationAngle = 8f;
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
        public void StartRotation()
        {
            StartCoroutine(Rotate(_rotationAngle, _rotationSpeed));
        }
        #endregion

        #region Internal Methods
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        private IEnumerator Rotate(float rotationAngle, float rotationSpeed)
        {
            float rotationProgress = 0f;
            float sinShift = Mathf.PI / 2;

            while (true)
            {
                rotationProgress += rotationSpeed * Time.deltaTime;
                float rotation = Mathf.Sin(rotationProgress - sinShift) * rotationAngle;
                transform.localRotation = Quaternion.Euler(0, 0, rotation);
                yield return null;
            }
        }
        #endregion
    }
}