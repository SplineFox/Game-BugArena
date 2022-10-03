using System;
using UnityEngine;

namespace SingleUseWorld
{
    public class PlayerGripHandler
    {
        #region Nested Classes
        [Serializable]
        public class Settings
        {
            public float MaxSlowDown = 0.5f;
            public float MaxDamagePerSecond = 2.5f;
        }
        #endregion

        #region Fields
        private Settings _settings;
        private PlayerSpeed _speed;
        private PlayerHealth _health;

        private float _totalSlowDown;
        private float _totalDamagePerSecond;
        #endregion

        #region Constructors
        public PlayerGripHandler(Settings settings, PlayerSpeed speed, PlayerHealth health)
        {
            _settings = settings;
            _speed = speed;
            _health = health;

            _totalSlowDown = 1f;
            _totalDamagePerSecond = 0f;
        }
        #endregion

        #region LifeCycle Methods
        public void Tick()
        {
            if(_totalDamagePerSecond != 0f)
            {
                _health.ApplyDamage(_totalDamagePerSecond * Time.deltaTime);
            }
        }
        #endregion

        #region Public Methods
        public void Grab(float grabbingSlowDown, float grabbingDamagePerSecond)
        {
            _totalSlowDown -= grabbingSlowDown;
            _totalDamagePerSecond += grabbingDamagePerSecond;

            _totalSlowDown = Mathf.Max(_totalSlowDown, _settings.MaxSlowDown);
            _totalDamagePerSecond = Mathf.Min(_totalDamagePerSecond, _settings.MaxDamagePerSecond);

            _speed.SetEnemyFactor(_totalSlowDown);
        }

        public void Release(float grabbingSlowDown, float grabbingDamagePerSecond)
        {
            _totalSlowDown += grabbingSlowDown;
            _totalDamagePerSecond -= grabbingDamagePerSecond;

            _totalSlowDown = Mathf.Min(_totalSlowDown, 1f);
            _totalDamagePerSecond = Mathf.Max(_totalDamagePerSecond, 0f);

            _speed.SetEnemyFactor(_totalSlowDown);
        }
        #endregion
    }
}