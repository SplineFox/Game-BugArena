using UnityEngine;

namespace SingleUseWorld
{
    public class TargetController
    {
        #region Constants
        private string CAMERA_TARGET_NAME = "CameraTarget";
        #endregion

        #region Fields
        private Transform _anchor;
        private GameObject _target;

        private CameraController _cameraController;
        private float _offset = 1.5f;
        #endregion

        #region Constructors
        public TargetController(CameraController cameraController)
        {
            _anchor = null;
            _cameraController = cameraController;

            _target = CreateTarget();
        }
        #endregion

        #region Public Methods
        public void SetAnchor(Transform anchor)
        {
            _anchor = anchor;
            _target.transform.SetParent(_anchor);
            _cameraController.SetTarget(_target.transform);
        }

        public void SetPosition(Vector2 normalizedPosition)
        {
            Vector3 offsetPosition = normalizedPosition * _offset;
            _target.transform.localPosition = offsetPosition;
        }

        public void ResetPosition()
        {
            _target.transform.localPosition = Vector3.zero;
        }
        #endregion

        #region Private Methods
        private GameObject CreateTarget()
        {
            var target = new GameObject();
            target.transform.Reset();
            target.name = CAMERA_TARGET_NAME;
            return target;
        }
        #endregion
    }
}