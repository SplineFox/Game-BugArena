using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace SingleUseWorld
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SkullEntityView : BodyView
    {
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Rotate(float angle, float duration)
        {
            StartCoroutine(RotateTo(angle, duration));
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

        private IEnumerator RotateTo(float angle, float duration)
        {
            var initalRotation = Vector3.forward * transform.eulerAngles.z;
            var targetRotation = Vector3.forward * angle;

            var elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                transform.eulerAngles = Vector3.Lerp(initalRotation, targetRotation, elapsedTime / duration);
                yield return null;
            }
        }
    }
}
