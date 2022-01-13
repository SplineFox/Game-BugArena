#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

namespace SingleUseWorld
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RigidbodyMovementComponent : ActorComponent
    {
        #region Fields
        private Rigidbody2D _rigidbody2D = default;

        private float _speed = 0f;
        private Vector2 _direction = Vector2.zero;
        private Vector2 _velocity = Vector2.zero;
        #endregion

        #region LifeCycle Methods
        public override void OnInitialize()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public override void OnFixedUpdate(float fixedDeltaTime)
        {
            _velocity = _direction * _speed;
            Vector2 translation = _velocity * fixedDeltaTime;
            Vector2 newPosition = _rigidbody2D.position + translation;
            _rigidbody2D.MovePosition(newPosition);
        }
        #endregion

        #region Public Methods
        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction.normalized;
        }
        #endregion

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            DrawDirection();
        }

        private void DrawDirection()
        {
            if (_direction.magnitude != 0)
            {
                Gizmos.color = Color.blue;
                GizmosExtension.DrawArrow(transform.position, _direction);
                Gizmos.color = Color.white;
            }
        }
#endif
    }
}