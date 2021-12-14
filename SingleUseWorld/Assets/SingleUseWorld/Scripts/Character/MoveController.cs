using UnityEngine;
using UnityEngine.Assertions;

namespace SingleUseWorld
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MoveController : MonoBehaviour
    {
        #region Fields
        [SerializeField] private float _moveSpeed;

        private Rigidbody2D _rigidbody2D;
        private Vector2 _velocity;
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
            _velocity = direction * _moveSpeed;
        }
        #endregion

        #region Private Methods
        private void CacheComponents()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            Assert.IsNotNull(_rigidbody2D, "\"Rigidbody2D\" is required.");
        }
        #endregion
    }
}