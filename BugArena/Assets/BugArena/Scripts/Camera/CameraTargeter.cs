using Cinemachine;
using UnityEngine;

namespace BugArena
{
    public class CameraTargeter : MonoBehaviour
    {
        #region Constants
        private string CAMERA_TARGET_NAME = "CameraTarget";
        #endregion

        #region Fields
        private CinemachineVirtualCamera _virtualCamera;

        private Transform _anchor;
        private GameObject _target;
        private float _offset = 1.5f;
        #endregion

        #region Constructors
        private void Awake()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();

            _anchor = null;
            _target = CreateTarget();
        }
        #endregion

        #region Public Methods
        public void SetAnchor(Transform anchor)
        {
            _anchor = anchor;
            _target.transform.SetParent(_anchor);
            _virtualCamera.Follow = _target.transform;
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