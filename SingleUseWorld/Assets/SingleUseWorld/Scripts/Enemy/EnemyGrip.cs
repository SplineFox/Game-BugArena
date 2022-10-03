using System;
using UnityEngine;

namespace SingleUseWorld
{
    public enum GripState
    {
        Grabbed,
        Released
    }

    [RequireComponent(typeof(Collider2D))]
    public class EnemyGrip : BaseComponent<GripState>
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
        #endregion

        #region LifeCycle Methods
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<IGrabbable>(out var grabbable))
            {
                _grabbableGameObject = collision.gameObject;
                _grabbable = grabbable;
                _grabbable.Grab(_settings.GrabbingSlowDown, _settings.GrabbingDamagePerSecond);
                SetState(GripState.Grabbed);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (_grabbableGameObject != null && collision.gameObject == _grabbableGameObject)
            {
                _grabbable.Release(_settings.GrabbingSlowDown, _settings.GrabbingDamagePerSecond);
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

            _grabbable.Release(_settings.GrabbingSlowDown, _settings.GrabbingDamagePerSecond);
            _grabbable = null;
            _grabbableGameObject = null;
            _state = GripState.Released;
        }
        #endregion
    }
}