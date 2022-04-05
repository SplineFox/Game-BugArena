using UnityEngine;

namespace SingleUseWorld
{
    public abstract class Item : MonoBehaviour
    {
        #region Fields
        private Elevator _elevator = default;
        private Projectile2D _projectile2D = default;
        private Rigidbody2D _rigidbody2D = default;
        private Collider2D _collider2D = default;
        #endregion

        #region LifeCycle Methods
        private void Start()
        {
            _elevator = GetComponent<Elevator>();
            _collider2D = GetComponent<Collider2D>();
            _projectile2D = GetComponent<Projectile2D>();

            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        #endregion

        #region Public Methods
        public void Attach(Transform target, float height)
        {
            transform.SetParent(target);
            transform.Reset();

            _elevator.height = height;
            _collider2D.enabled = false;

            _projectile2D.ResetVelocity();
            _projectile2D.IsKinematic = true;
            _projectile2D.enabled = false;

            _rigidbody2D.isKinematic = true;
        }

        public void Detach()
        {
            transform.SetParent(null);

            _elevator.height = 0f;
            _collider2D.enabled = true;

            _projectile2D.IsKinematic = false;
            _projectile2D.enabled = true;

            _rigidbody2D.isKinematic = false;
        }

        public abstract void Use(Vector2 direction);
        #endregion
    }
}