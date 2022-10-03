using System;
using UnityEngine;

namespace SingleUseWorld
{
    public sealed class PlayerSpeed
    {
        #region Nested Classes
        [Serializable]
        public class Settings
        {
            public float InitialSpeed = 4f;
            public float MinSpeed = 1.5f;
        }
        #endregion

        #region Fields
        private float _speed;
        private float _enemyFactor;
        private float _itemFactor;

        private Settings _settings;
        private ProjectileMovement2D _movement;
        #endregion

        #region Properties
        public float Value
        {
            get => _speed;
        }
        #endregion

        #region Constructors
        public PlayerSpeed(Settings settings, ProjectileMovement2D movement)
        {
            _settings = settings;
            _movement = movement;
            _speed = settings.InitialSpeed;

            _enemyFactor = 1f;
            _itemFactor = 1f;

            _movement.SetSpeed(settings.InitialSpeed);
        }
        #endregion

        #region Public Methods
        public void SetEnemyFactor(float enemyFactor)
        {
            _enemyFactor = enemyFactor;
            UpdateSpeed();
        }

        public void SetItemFactor(float itemFactor)
        {
            _itemFactor = itemFactor;
            UpdateSpeed();
        }

        public void ResetEnemyFactor()
        {
            SetEnemyFactor(1f);
        }

        public void ResetItemFactor()
        {
            SetItemFactor(1f);
        }
        #endregion

        #region Private Methods
        private void UpdateSpeed()
        {
            var newSpeed = _settings.InitialSpeed * _enemyFactor * _itemFactor;
            newSpeed = Math.Max(_settings.MinSpeed, newSpeed);

            if (_speed == newSpeed)
                return;

            _speed = newSpeed;
            _movement.SetSpeed(newSpeed);
        }
        #endregion
    }
}