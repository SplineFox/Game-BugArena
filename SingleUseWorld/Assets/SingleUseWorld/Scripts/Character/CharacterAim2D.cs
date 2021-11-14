using UnityEngine;
using UnityEngine.Assertions;

namespace SingleUseWorld
{
    public class CharacterAim2D : MonoBehaviour
    {
        #region Constants
        private string CAMERA_TARGET_NAME = "CameraTarget";
        #endregion

        #region Fields
        private Camera _mainCamera = default;
        private GameObject _cameraTarget = default;
        private CharacterInput _characterInput = default;

        [SerializeField] private GameObject _crosshair;
        [SerializeField] private Vector3 _crosshairPosition;
        [SerializeField] private Vector2 _aimDirection;
        [SerializeField] private float _aimAngle;

        [SerializeField] private bool _moveCameraTarget = true;
        [SerializeField] private float _cameraTargetSpeed = 5f;
        [SerializeField] private float _cameraTargetDistance = 10f;
        
        private Vector3 _newCameraTargetPosition;
        private Vector3 _newCameraTargetDirection;
        #endregion

        #region Properties
        public GameObject CameraTarget { get => _cameraTarget; }
        public Vector2 AimDirection { get => _aimDirection; }
        public float AimAngle { get => _aimAngle; }
        #endregion

        #region LifeCycle Methods
        private void Awake()
        {
            Cursor.visible = false;
            CacheComponents();
        }

        private void Update()
        {
            UpdateMouseAim();
            UpdateCameraTarget();
        }
        #endregion

        #region Private Methods
        private void CacheComponents()
        {
            _mainCamera = Camera.main;
            _characterInput = GetComponent<CharacterInput>();
            Assert.IsNotNull(_characterInput, "\"CharacterInput\" is required.");

            _cameraTarget = transform.Find(CAMERA_TARGET_NAME)?.gameObject;
            if (_cameraTarget == null) 
                _cameraTarget = CreateCameraTarget();
        }

        private GameObject CreateCameraTarget()
        {
            var cameraTarget = new GameObject();
            cameraTarget.transform.SetParent(this.transform);
            cameraTarget.transform.localPosition = Vector3.zero;
            cameraTarget.name = CAMERA_TARGET_NAME;
            return cameraTarget;
        }

        private void UpdateMouseAim()
        {
            Vector2 mouseScreenPosition = _characterInput.LookInput;
            Vector3 mouseWolrdPosition = _mainCamera.ScreenToWorldPoint(mouseScreenPosition);
            mouseWolrdPosition.z = transform.position.z;

            _crosshairPosition = mouseWolrdPosition;
            _aimDirection = _crosshairPosition - transform.position;
            _aimAngle = Mathf.Atan2(AimDirection.y, AimDirection.x) * Mathf.Rad2Deg;

            Quaternion angle = Quaternion.Euler(0, 0, AimAngle);
            Vector3 rotation = angle * Vector3.up;

            _crosshair.transform.position = _crosshairPosition;
            _crosshair.transform.localRotation = angle;
        }

        private void UpdateCameraTarget()
        {
            if (!_moveCameraTarget)
                return;

            _newCameraTargetPosition = _crosshairPosition;
            _newCameraTargetDirection = _newCameraTargetPosition - this.transform.position;
            if(_newCameraTargetDirection.magnitude > _cameraTargetDistance)
            {
                _newCameraTargetDirection = _newCameraTargetDirection.normalized * _cameraTargetDistance;
            }
            _newCameraTargetPosition = this.transform.position + _newCameraTargetDirection;
            _newCameraTargetPosition = Vector3.Lerp(_cameraTarget.transform.position, _newCameraTargetPosition, Time.deltaTime * _cameraTargetSpeed);
            
            _cameraTarget.transform.position = _newCameraTargetPosition;
        }
        #endregion
    }
}