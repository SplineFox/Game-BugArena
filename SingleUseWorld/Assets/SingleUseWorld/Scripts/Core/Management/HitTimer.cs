using System.Collections;
using UnityEngine;

namespace SingleUseWorld
{
    public class HitTimer : MonoBehaviour
    {
        #region Fields
        private bool _isWaiting;
        #endregion

        #region Properties
        #endregion

        #region LifeCycle Methods
        #endregion

        #region Public Methods
        public void StopTime(float duration)
        {
            if (_isWaiting)
                return;
            Time.timeScale = 0.0f;
            StartCoroutine(Wait(duration));
        }
        #endregion

        #region Private Methods
        private IEnumerator Wait(float duration)
        {
            _isWaiting = true;
            yield return new WaitForSecondsRealtime(duration);
            Time.timeScale = 1.0f;
            _isWaiting = false;
        }
        #endregion
    }
}