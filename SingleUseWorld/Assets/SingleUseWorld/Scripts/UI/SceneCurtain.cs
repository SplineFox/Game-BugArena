using System;
using System.Collections;
using UnityEngine;

namespace SingleUseWorld
{
    public class SceneCurtain : MonoBehaviour
    {
        [SerializeField] private CircularCurtain _circularCurtain;
        [SerializeField] [Range(1f, 5f)] private float _duration;

        public void Open(Vector2 screenPoint, Action onCurtainOpened = null)
        {
            StartCoroutine(Scale(0f, 1f, screenPoint, _duration));
            onCurtainOpened?.Invoke();
        }

        public void Close(Vector2 screenPoint, Action onCurtainClosed = null)
        {
            StartCoroutine(Scale(1f, 0f, screenPoint, _duration));
            onCurtainClosed?.Invoke();
        }

        private IEnumerator Scale(float fromRadius, float toRadius, Vector2 targetPoint, float duration)
        {
            _circularCurtain.SetCenter(targetPoint);

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
        }


    }
}