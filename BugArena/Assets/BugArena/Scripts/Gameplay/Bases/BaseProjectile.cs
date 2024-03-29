using UnityEngine;

namespace BugArena
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Projectile2D))]
    public class BaseProjectile : BaseBehaviour
    {
        #region Fields
        protected Collider2D _collider = default;
        protected Rigidbody2D _rigidbody = default;
        protected Projectile2D _projectile = default;
        #endregion

        #region LifeCycle Methods
        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<Collider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _projectile = GetComponent<Projectile2D>();
        }
        #endregion
    }
}