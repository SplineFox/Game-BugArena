using UnityEngine;

namespace SingleUseWorld
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ShadowView : ElevatorObserver
    {
        #region Fields
        private SpriteRenderer _spriteRenderer = default;

        [Min(0f)] [SerializeField] private float _maxVisibleHeight = 1f;
        [Range(0f, 1f)] [SerializeField] private float _minTransparency = 1f;
        [Range(0f, 1f)] [SerializeField] private float _maxTransparency = 0.2f;
        #endregion

        #region LifeCycle Methods
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        #endregion

        #region Public Methods
        public override void UpdateHeight(float height)
        {
            var normalizedHeight = Mathf.InverseLerp(0f, _maxVisibleHeight, height);
            UpdateScale(normalizedHeight);
            UpdateTransparency(normalizedHeight);
        }
        #endregion

        #region Private Methods
        private void UpdateScale(float normalizedHeight)
        {
            var newScale = Vector3.one * (1f - normalizedHeight);
            transform.localScale = newScale;
        }

        private void UpdateTransparency(float normalizedHeight)
        {
            var transparency = Mathf.Lerp(_minTransparency, _maxTransparency, normalizedHeight);
            var newColor = _spriteRenderer.color;
            newColor.a = transparency;
            _spriteRenderer.color = newColor;
        }
        #endregion
    }
}