using System;
using UnityEngine;

namespace BugArena
{
    public sealed class EnemyHealth
    {
        #region Nested Classes
        [Serializable]
        public class Settings
        {
            public float InitialHealth = 100f;
        }
        #endregion

        #region Fields
        private float _health;
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
        public Action<Damage> Died = delegate { };
        #endregion

        #region Constructors
        public EnemyHealth(Settings settings)
        {
            _settings = settings;
            _health = settings.InitialHealth;
        }
        #endregion

        #region Public Methods
        public void TakeDamage(Damage damage)
        {
            if (IsDead)
                return;

            var newHealth = _health - damage.amount;
            _health = Mathf.Max(0f, newHealth);

            if (IsDead)
            {
                Died.Invoke(damage);
                return;
            }
        }

        public void OnReset()
        {
            _health = _settings.InitialHealth;
        }
        #endregion
    }
}