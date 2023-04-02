using UnityEngine;

namespace BugArena
{
    public class MouseAim
    {
        #region Fields
        private Camera _camera = default;
        private Transform _anchor = default;

        // Imitates the physical boundary of a thumbstick
        // within which the value is normalized.
        private float _virtualMovementBoundary = 1.5f;

        private Vector3 _mouseWorldPosition = Vector3.zero;
        private Vector2 _aimValue = Vector2.zero;
        private Vector2 _aimDirection = Vector2.zero;
        #endregion

        #region Properties
        public Vector3 MouseWorldPosition { get => _mouseWorldPosition; }
        public Vector2 AimValue { get => _aimValue; }
        public Vector2 AimDirection { get => _aimDirection; }
        #endregion

        #region Constructors
        public MouseAim(Camera camera)
        {
            _camera = camera;
            _anchor = null;
        }
        #endregion

        #region Public Methods
        public void SetAnchor(Transform anchor)
        {
            _anchor = anchor;
        }

        public void Update(Vector2 mouseScreenPosition)
        {
            if (!_anchor)
                return;

            _mouseWorldPosition = _camera.ScreenToWorldPoint(mouseScreenPosition);
            _mouseWorldPosition.z = _anchor.position.z;

            _aimValue = (_mouseWorldPosition - _anchor.position);
            _aimDirection = _aimValue.normalized;

            if (_aimValue.magnitude > _virtualMovementBoundary)
            {
                _aimValue = _aimDirection;
            }
            else
            {
                _aimValue /= _virtualMovementBoundary;
            }
        }
        #endregion
    }
}