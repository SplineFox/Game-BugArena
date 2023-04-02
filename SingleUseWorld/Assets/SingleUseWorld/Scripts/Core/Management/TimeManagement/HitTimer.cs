using System.Collections;
using UnityEngine;

namespace SingleUseWorld
{
    public class HitTimer : IHitTimer
    {
        #region Fields
        private readonly ICoroutineRunner _coroutineRunner;

        private bool _isWaiting;
        private Coroutine _waitCoroutine;
        #endregion

        #region Constructors
        public HitTimer(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }
        #endregion

        #region Public Methods
        public void StopTime(float duration)
        {
            if (_isWaiting)
                return;

            _isWaiting = true;
            Time.timeScale = 0.0f;
            _waitCoroutine = _coroutineRunner.StartCoroutine(Wait(duration));
        }

        public void ResumeTime()
        {
            _isWaiting = false;
            Time.timeScale = 1.0f;
            _coroutineRunner.StopCoroutine(_waitCoroutine);
        }
        #endregion

        #region Private Methods
        private IEnumerator Wait(float duration)
        {
            yield return new WaitForSecondsRealtime(duration);
            Time.timeScale = 1.0f;
            _isWaiting = false;
        }

        public void Pause()
        {
        }

        public void UnPause()
        {
            
        }
        #endregion
    }
}