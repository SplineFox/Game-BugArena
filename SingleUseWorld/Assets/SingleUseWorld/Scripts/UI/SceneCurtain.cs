using System;
using System.Collections;
using UnityEngine;

namespace SingleUseWorld
{
    public class SceneCurtain : MonoBehaviour
    {
        [SerializeField] private CircularCurtain _circularCurtain;
        [SerializeField] [Range(1f, 5f)] private float _duration;

        public void Open(Action onCurtainOpened = null)
        {
            StartCoroutine(Scale(0f, 1f, _duration, onCurtainOpened));
        }

        public void Close(Action onCurtainClosed = null)
        {
            StartCoroutine(Scale(1f, 0f, _duration, onCurtainClosed));
        }

        private IEnumerator Scale(float fromRadius, float toRadius, float duration, Action onScaleFinished = null)
        {
            var radius = 0f;
            var elapsedTime = 0f;
            while (elapsedTime <= duration)
            {
                radius = Mathf.Lerp(fromRadius, toRadius, elapsedTime / duration);
                _circularCurtain.SetRadius(radius);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            _circularCurtain.SetRadius(toRadius);
            onScaleFinished?.Invoke();
        }


    }
}