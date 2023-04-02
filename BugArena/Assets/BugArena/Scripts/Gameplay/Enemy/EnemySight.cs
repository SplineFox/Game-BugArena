using System;
using UnityEngine;

namespace BugArena
{
    public enum SightState
    {
        InSight,
        OutSight
    }

    [RequireComponent(typeof(Collider2D))]
    public class EnemySight : BaseComponent<SightState>
    {
        #region Nested Classes
        [Serializable]
        public class Settings
        {
            public float OutSightRadius = 2f;
            public float InSightRadius = 3f;
        }
        #endregion

        #region Fields
        private CircleCollider2D _collider2D = default;
        private Transform _target = default;
        private Settings _settings;
        private bool _sightAllowed = true;
        #endregion

        #region Properties
        public bool SightAllowed
        { 
            get => _sightAllowed;
            set
            {
                if (_sightAllowed == value)
                    return;

                _sightAllowed = value;
                if (_sightAllowed)
                    EnableTrigger();
                else
                    DisableTrigger();
            }
        }

        public Transform Target { get => _target; }

        public Vector2 DirectionToTarget
        {
            get
            {
                if (_target == null)
                    return Vector2.zero;

                return (_target.position - transform.position).normalized;
            }
        }
        #endregion

        #region LifeCycle Methods
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<Player>(out var player))
            {
                _target = player.transform;
                _collider2D.radius = _settings.InSightRadius;
                SetState(SightState.InSight);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(_target != null && _target.gameObject == collision.gameObject)
            {
                _target = null;
                _collider2D.radius = _settings.OutSightRadius;
                SetState(SightState.OutSight);
            }
        }
        #endregion

        #region Public Methods
        public void Initialize(Settings settings)
        {
            _settings = settings;
            _collider2D = GetComponent<CircleCollider2D>();
            _collider2D.radius = _settings.OutSightRadius;
            _state = SightState.OutSight;
        }
        #endregion

        #region Private Methods
        private void EnableTrigger()
        {
            _collider2D.enabled = true;
        }

        private void DisableTrigger()
        {
            _target = null;
            _state = SightState.OutSight;
            _collider2D.enabled = false;
        }
        #endregion
    }
}