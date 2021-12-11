#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.Assertions;
using UnityAtoms.BaseAtoms;

namespace SingleUseWorld
{
    public class AimController : MonoBehaviour
    {
        #region Constants
        private string CAMERA_TARGET_NAME = "CameraTarget";
        #endregion

        #region Fields
        [SerializeField]
        private Vector2Variable _mouseMoveInput;
        
        [SerializeField]
        private GameObject _crosshairPrefab;

        [SerializeField, Range(0f, 5f)]
        private float _cameraTargetMaxDistance = 1f;

        private Camera _mainCamera = default;
        private GameObject _crosshair = default;
        private GameObject _cameraTarget = default;
        #endregion

        #region LifeCycle Methods
        private void Awake()
        {
            CacheComponents();
            Initialize();
        }

        private void FixedUpdate()
        {
            Vector3 mouseWorldPosition = GetMouseWorldPosition(_mouseMoveInput.Value);
            MoveCrosshair(mouseWorldPosition);
            MoveCameraTarget(mouseWorldPosition);
        }
        #endregion

        #region Private Methods
        private void CacheComponents()
        {
            _mainCamera = Camera.main;
            Assert.IsNotNull(_mainCamera, "\"Main Camera\" is required.");
            Assert.IsNotNull(_mouseMoveInput, "\"MouseMoveInput Variable\" is required.");
        }

        private void Initialize()
        {
            InitializeCrosshair();
            InitializeCameraTarget();
        }

        private void InitializeCrosshair()
        {
            _crosshair = (GameObject)Instantiate(_crosshairPrefab);
        }

        private void InitializeCameraTarget()
        {
            _cameraTarget = transform.Find(CAMERA_TARGET_NAME)?.gameObject;
            if(_cameraTarget == null)
            {
                _cameraTarget = new GameObject();
                _cameraTarget.name = CAMERA_TARGET_NAME;
                _cameraTarget.transform.SetParent(this.transform);
                _cameraTarget.transform.localPosition = Vector3.zero;
            }
        }

        private Vector3 GetMouseWorldPosition(Vector2 mouseScreenPosition)
        {
            Vector3 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(mouseScreenPosition);
            mouseWorldPosition.z = transform.position.z;
            return mouseWorldPosition;
        }

        private void MoveCrosshair(Vector2 mouseWorldPosition)
        {
            _crosshair.transform.position = mouseWorldPosition;
        }

        private void MoveCameraTarget(Vector2 mouseWorldPosition)
        {
            Vector3 cameraTargetPosition = mouseWorldPosition;
            Vector3 cameraTargetDistance = cameraTargetPosition - transform.position;
            if (cameraTargetDistance.magnitude > _cameraTargetMaxDistance)
            {
                cameraTargetPosition = transform.position + (cameraTargetDistance.normalized * _cameraTargetMaxDistance);
            }
            _cameraTarget.transform.position = cameraTargetPosition;
        }
        #endregion

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            DrawCameraTargetMaxDistance();
        }

        private void DrawCameraTargetMaxDistance()
        {
            Handles.color = Color.gray;
            Handles.DrawWireDisc(transform.position, transform.forward, _cameraTargetMaxDistance);
            Handles.color = Color.white;
        }
#endif
    }
}