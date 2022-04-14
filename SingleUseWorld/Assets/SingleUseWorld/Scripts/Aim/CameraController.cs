using Cinemachine;
using UnityEngine;

namespace SingleUseWorld
{
    public class CameraController
    {
        #region Fields
        private Camera _camera;
        private CinemachineVirtualCamera _virtualCamera;
        #endregion

        #region Properties
        public Camera Camera { get => _camera; }
        #endregion

        #region Constructors
        public CameraController(Camera camera, CinemachineVirtualCamera virtualCamera)
        {
            _camera = camera;
            _virtualCamera = virtualCamera;
        }
        #endregion

        #region Public Methods
        public void SetTarget(Transform target)
        {
            _virtualCamera.Follow = target;
        }
        #endregion
    }
}