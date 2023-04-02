using System;
using UnityEngine;

namespace BugArena
{
    public enum GripState
    {
        Grabbed,
        Released
    }

    [RequireComponent(typeof(Collider2D))]
    public class EnemyGrip : BaseComponent<GripState>, IGrabber
    {
        #region Nested Classes
        [Serializable]
        public class Settings
        {
            public float GrabbingDamagePerSecond = 0.5f;
            public float GrabbingSlowDown = 0.5f;
        }
        #endregion

        #region Fields
        private Collider2D _collider2D = default;
        private GameObject _grabbableGameObject;
        private IGrabbable _grabbable;
        private Settings _settings;
        private bool _gripAllowed = true;
        #endregion

        #region Properties
        public bool GripAllowed
        {
            get => _gripAllowed;
            set
            {
                if (_gripAllowed == value)
                    return;

                _gripAllowed = value;
                if (_gripAllowed)
                    EnableTrigger();
                else
                    DisableTrigger();
            }
        }

        float IGrabber.DamagePerSecond
        {
            get => _settings.GrabbingDamagePerSecond;
        }

        float IGrabber.SlowDown
        {
            get => _settings.GrabbingSlowDown;
        }
        #endregion

        #region LifeCycle Methods
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<IGrabbable>(out var grabbable))
            {
                _grabbableGameObject = collision.gameObject;
                _grabbable = grabbable;
                _grabbable.GrabbedBy(this);
                SetState(GripState.Grabbed);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (_grabbableGameObject != null && collision.gameObject == _grabbableGameObject)
            {
                _grabbable.ReleasedBy(this);
                _grabbable = null;
                _grabbableGameObject = null;
                SetState(GripState.Released);
            }
        }
        #endregion

        #region Public Methods
        public void Initialize(Settings settings)
        {
            _grabbable = null;
            _settings = settings;
            _collider2D = GetComponent<Collider2D>();
            _state = GripState.Released;
        }

        public void Release()
        {
            if (_grabbable == null)
                return;

            _grabbable.ReleasedBy(this);
            _grabbable = null;
            _grabbableGameObject = null;
            _state = GripState.Released;
        }
        #endregion

        #region Private Methods
        private void EnableTrigger()
        {
            _collider2D.enabled = true;
        }

        private void DisableTrigger()
        {
            Release();
            _collider2D.enabled = false;
        }
        #endregion
    }
}