using UnityEngine;

namespace SingleUseWorld
{
    public class EffectAppearanceBuilder
    {
        #region Fields
        private Vector3 _position;
        private Vector3 _offset;
        private Vector3 _rotation;
        private Vector3 _velocity;
        private float _playbackStart;
        #endregion

        #region Constructors
        public EffectAppearanceBuilder()
        {
            Reset();
        }
        #endregion

        #region Public Methods
        public void Reset()
        {
            _position = Vector3.zero;
            _offset = Vector3.zero;
            _rotation = Vector3.zero;
            _velocity = Vector3.zero;
            _playbackStart = 0f;
        }

        public EffectAppearanceBuilder WithPosition(Vector3 position)
        {
            _position = position;
            return this;
        }
        public EffectAppearanceBuilder WithRandomOffset(float radius)
        {
            var x = Random.Range(-1f, 1f) * radius;
            var y = Random.Range(-1f, 1f) * radius;
            _offset = new Vector3(x, y, 0f);
            return this;
        }

        public EffectAppearanceBuilder WithRandomRotation()
        {
            float rotation = 90f * Random.Range(0, 3);
            _rotation = new Vector3(0f, 0f, rotation);
            return this;
        }

        public EffectAppearanceBuilder WithRandomVelocity(Vector3 direction, float thresholdSpeed)
        {
            float speed = Random.Range(0f, thresholdSpeed);
            _velocity = direction * speed;
            return this;
        }

        public EffectAppearanceBuilder WithRandomPlaybackTime(float playbackThresholdTime)
        {
            float playbackStart = Random.Range(0f, playbackThresholdTime);
            _playbackStart = playbackStart;
            return this;
        }

        public EffectAppearance Build()
        {
            return new EffectAppearance()
            {
                position = _position + _offset,
                rotation = _rotation,
                velocity = _velocity,
                playbackStart = _playbackStart
            };
        }
        #endregion
    }
}