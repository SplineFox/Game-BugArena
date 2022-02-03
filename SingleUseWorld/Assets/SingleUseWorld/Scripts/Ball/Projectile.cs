using UnityEngine;

namespace SingleUseWorld
{
    [RequireComponent(typeof(Elevator), typeof (ProjectileMovement))]
    public abstract class Projectile: MonoBehaviour
    {
        #region Properties
        protected Elevator _elevator { get; private set; }
        protected ProjectileMovement _movement { get; private set; }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Derived classes must provide their own Projectile-based view.
        /// </summary>
        protected abstract ProjectileView _projectileView { get; }

        protected virtual void Start()
        {
            _elevator = GetComponent<Elevator>();
            _movement = GetComponent<ProjectileMovement>();
        }

        protected virtual void Update()
        {
            _projectileView.UpdateHeightPresentation(_elevator.height);
        }

        protected virtual void FixedUpdate()
        {
        }
        #endregion
    }
}