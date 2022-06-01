using System;
using UnityEngine;

namespace SingleUseWorld
{
    public class Cooldown
    {
        #region Fields
        private bool _isActive;
        private float _durationTime;
        private float _remainingTime;
        #endregion

        #region Properties
        public bool IsActive { get => _isActive; }
        public bool IsCompleted { get => _remainingTime <= 0f; }
        public float DurationTime { get => _durationTime; }
        public float RemainingTime { get => _remainingTime; }
        public float ElapsedTime { get => _durationTime - _remainingTime; }
        #endregion

        #region Delegates & Events
        public Action Completed = delegate { };
        #endregion

        #region Constructors
        public Cooldown(float durationTime)
        {
            _isActive = false;
            _durationTime = durationTime;
            _remainingTime = 0f;
        }
        #endregion

        #region Public Methods
        public void Start()
        {
            _isActive = true;
            _remainingTime = _durationTime;
            StopIfCompleted();
        }

        public void Update(float deltaTime)
        {
            if(IsActive && !IsCompleted)
            {
                _remainingTime -= deltaTime;
                StopIfCompleted();
            }
        }

        public void Stop()
        {
            _remainingTime = 0f;
            _isActive = false;
        }
        #endregion

        #region Private Methods
        private void StopIfCompleted()
        {
            if (IsCompleted)
            {
                Stop();
                Completed.Invoke();
            }
        }
        #endregion
    }
}