using System.Collections;
using UnityEngine;

namespace SingleUseWorld
{
    public class HitTimer : MonoBehaviour
    {
        #region Fields
        private bool _isWaiting;
        #endregion

        #region Public Methods
        public void StopTime(float duration)
        {
            if (_isWaiting)
                return;

            _isWaiting = true;
            Time.timeScale = 0.0f;
            StartCoroutine(Wait(duration));
        }

        public void ResumeTime()
        {
            _isWaiting = false;
            Time.timeScale = 1.0f;
            StopAllCoroutines();
        }
        #endregion

        #region Private Methods
        private IEnumerator Wait(float duration)
        {
            yield return new WaitForSecondsRealtime(duration);
            Time.timeScale = 1.0f;
            _isWaiting = false;
        }
        #endregion
    }
}