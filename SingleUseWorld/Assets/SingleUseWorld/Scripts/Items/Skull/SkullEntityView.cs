using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace SingleUseWorld
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SkullEntityView : BodyView
    {
        private SpriteRenderer _spriteRenderer;
        protected Coroutine _spinCoroutine;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        public void StartSpin(float spinSpeedPerSecond)
        {
            StopSpin();
            _spinCoroutine = StartCoroutine(Spin(spinSpeedPerSecond));
        }

        public void StopSpin()
        {
            if (_spinCoroutine != null)
                StopCoroutine(_spinCoroutine);
        }

        public void FadeIn(float duration)
        {
            StartCoroutine(FadeTo(1f, duration));
        }

        public void FadeOut(float duration)
        {
            StartCoroutine(FadeTo(0f, duration));
        }

        private IEnumerator FadeTo(float opacity, float duration)
        {
            opacity = Mathf.Clamp01(opacity);
            var initialColor = _spriteRenderer.color;
            var targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, opacity);

            var elapsedTime = 0f;
            while(elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                _spriteRenderer.color = Color.Lerp(initialColor, targetColor, elapsedTime / duration);
                yield return null;
            }
        }

        protected IEnumerator Spin(float spinSpeedPerSecond)
        {
            while (true)
            {
                var spinDelta = spinSpeedPerSecond * Time.deltaTime * Vector3.forward;
                transform.Rotate(spinDelta);
                yield return null;
            }
        }
    }
}
