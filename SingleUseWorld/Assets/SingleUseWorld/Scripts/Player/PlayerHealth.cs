using System;
using System.Collections;
using UnityEngine;

namespace SingleUseWorld
{
    public sealed class PlayerHealth
    {
        #region Nested Classes
        [Serializable]
        public class Settings
        {
            public float InitialHealth = 100f;
            public float TimeTillRecovery = 0.25f;
            public float RecoveryDuration = 0.3f;
        }
        #endregion

        #region Fields
        private float _health;
        private Coroutine _recoveryCoroutine;

        private Player _player;
        private Settings _settings;
        #endregion

        #region Properties
        public float Value
        {
            get => _health;
        }

        public bool IsDead
        {
            get => _health == 0;
        }
        #endregion

        #region Delegates & Events
        public Action Died = delegate { };
        #endregion

        #region Constructors
        public PlayerHealth(Settings settings, Player player)
        {
            _settings = settings;
            _player = player;
            _health = settings.InitialHealth;
        }
        #endregion

        #region Public Methods
        public void ApplyDamage(float damage)
        {
            if (IsDead)
                return;

            if (_recoveryCoroutine != null)
                _player.StopCoroutine(_recoveryCoroutine);

            var newHealth = _health - damage;
            _health = Mathf.Max(0f, newHealth);

            if (IsDead)
            {
                Died.Invoke();
                return;
            }

            _recoveryCoroutine = _player.StartCoroutine(RecoverHealth(_settings.RecoveryDuration));
        }
        #endregion

        #region Private Methods
        private IEnumerator RecoverHealth(float duration)
        {
            yield return new WaitForSeconds(_settings.TimeTillRecovery);

            var initialHealth = _health;
            var targetHealth = _settings.InitialHealth;

            var elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                _health = Mathf.Lerp(initialHealth, targetHealth, elapsedTime / duration);
                yield return null;
            }
            _recoveryCoroutine = null;
        }
        #endregion
    }
}