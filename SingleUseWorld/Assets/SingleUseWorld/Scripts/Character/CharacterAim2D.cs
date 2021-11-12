using UnityEngine;
using UnityEngine.Assertions;

namespace SingleUseWorld
{
    public class CharacterAim2D : MonoBehaviour
    {
        #region Fields
        private Camera _mainCamera = default;
        private CharacterInput _characterInput = default;

        [SerializeField] private GameObject _crosshair;
        [SerializeField] private Vector2 _aimDirection;
        [SerializeField] private float _aimAngle;
        #endregion

        #region Properties
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
        }
        #endregion

        #region Private Methods
        private void CacheComponents()
        {
            _mainCamera = Camera.main;
            _characterInput = GetComponent<CharacterInput>();
            Assert.IsNotNull(_characterInput, "\"CharacterInput\" is required.");
        }

        private void UpdateMouseAim()
        {
            Vector2 mouseScreenPosition = _characterInput.LookInput;
            Vector3 mouseWolrdPosition = _mainCamera.ScreenToWorldPoint(mouseScreenPosition);
            mouseWolrdPosition.z = transform.position.z;
           
            _aimDirection = mouseWolrdPosition - transform.position;
            _aimAngle = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * Mathf.Rad2Deg;

            Quaternion angle = Quaternion.Euler(0, 0, _aimAngle);
            Vector3 rotation = angle * Vector3.up;

            _crosshair.transform.position = mouseWolrdPosition;
            _crosshair.transform.localRotation = angle;
        }
        #endregion
    }
}