using UnityEngine;
using UnityEngine.Assertions;

namespace SingleUseWorld
{
    public class CharacterController2D : MonoBehaviour
    {
        #region Fields
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
            Vector2 transition = _velocity * Time.fixedDeltaTime;
            Vector2 position = _rigidbody2D.position + transition;
            _rigidbody2D.MovePosition(position);
        }
        #endregion

        #region Public Methods
        public virtual void SetVelocity(Vector2 velocity)
        {
            _velocity = velocity;
        }

        public virtual void MovePosition(Vector2 position)
        {
            _rigidbody2D.MovePosition(position);
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