using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace SingleUseWorld
{
    public class ScoreCounter: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counter;

        [SerializeField] private RangeFloat _scoreRangeFactor;
        [SerializeField] private RangeFloat _scaleRange;
        [SerializeField] [Range(0.1f, 0.9f)] private float _countAnimationDuration = 0.6f;
        [SerializeField] [Range(1.1f, 1.5f)] private float _scaleAnimationDuration = 0.4f;

        private Score _score;
        private int _currentScore;

        public void Construct(Score score)
        {
            _score = score;
            _currentScore = _score.Points;
        }

        private void Start()
        {
            ResetCounter();
            _score.Changed += UpdateCounter;
        }

        public void ResetCounter()
        {
            _currentScore = _score.Points;
            SetCounterText(_score.Points);
        }

        private void UpdateCounter()
        {
            var scoreDifference = _score.Points - _currentScore;
            var scale = MathfUtil.Remap(scoreDifference, _scoreRangeFactor, _scaleRange);
            
            StopAllCoroutines();
            StartCoroutine(CountAnimation(_countAnimationDuration));
            StartCoroutine(ScaleAnimation(_scaleAnimationDuration, scale));
        }

        private void SetCounterText(int score)
        {
            _counter.text = $"{score}p";
        }

        private IEnumerator CountAnimation(float duration)
        {
            var initialScore = _currentScore;
            var targetScore = _score.Points;
            
            var elapsedTime = 0f;
            while (elapsedTime <= duration)
            {
                elapsedTime += Time.deltaTime;
                _currentScore = (int)Mathf.Lerp(initialScore, targetScore, EaseOut(elapsedTime / duration));
                SetCounterText(_currentScore);
                yield return null;
            }
            _currentScore = targetScore;
            SetCounterText(_currentScore);
        }

        private IEnumerator ScaleAnimation(float duration, float scale)
        {
            var initialScale = _counter.transform.localScale;
            var targetScale = new Vector3(scale, scale, 1f);

            var elapsedTime = 0f;
            while (elapsedTime <= duration)
            {
                elapsedTime += Time.deltaTime;
                _counter.transform.localScale = Vector3.Lerp(initialScale, targetScale, BackAndForth(elapsedTime/duration));
                yield return null;
            }
            _counter.transform.localScale = Vector3.one;
        }

        private float EaseIn(float t)
        {
            return t * t;
        }

        private float EaseOut(float t)
        {
            return 1 - (1 - t) * (1 - t);
        }

        private float EaseInOut(float t)
        {
            return Mathf.Lerp(EaseIn(t), EaseOut(t), t);
        }

        private float Flip(float t)
        {
            return 1f - t;
        }

        private float BackAndForth(float t)
        {
            if (t <= 0.5f) 
                return EaseOut(t / 0.5f);

            return EaseIn(Flip(t) / 0.5f);
        }
    }
}