using UnityEngine;
using UnityEngine.Assertions;
using UnityAtoms.BaseAtoms;

namespace SingleUseWorld
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMovement : MonoBehaviour
    {
        #region Fields
        private Rigidbody2D _rigidbody2D;
        private Vector2 _velocity;

        [SerializeField] private FloatConstant _moveSpeed;
        #endregion

        #region LifeCycle Methods
        private void Awake()
        {
            CacheComponents();
        }

        private void FixedUpdate()
        {
            Vector2 translation = _velocity * Time.fixedDeltaTime;
            Vector2 position = _rigidbody2D.position + translation;
            _rigidbody2D.MovePosition(position);
        }
        #endregion

        #region Public Methods
        public void CalculateVelocity(Vector2 moveInput)
        {
            Vector2 direction = moveInput;
            _velocity = direction * _moveSpeed.Value;
        }
        #endregion

        #region Private Methods
        private void CacheComponents()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            Assert.IsNotNull(_rigidbody2D, "\"Rigidbody2D\" is required.");
            Assert.IsNotNull(_moveSpeed, "\"MoveSpeed Constant\" is required.");
        }
        #endregion
    }
}