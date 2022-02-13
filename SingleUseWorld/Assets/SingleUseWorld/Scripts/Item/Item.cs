using UnityEngine;

namespace SingleUseWorld
{
    public class Item : Projectile
    {
        #region Fields
        [SerializeField] private ItemView _itemView = default;
        #endregion

        #region Properties
        protected override ProjectileView _projectileView => _itemView;
        #endregion

        #region Public Methods
        public void Attach(Transform target, float height)
        {
            transform.SetParent(target);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
            transform.localScale = Vector3.one;

            _elevator.height = height;
            _movement.IsKinematic = true;
            _movement.ResetVelocity();
        }

        public void Detach()
        {
            transform.SetParent(null);
            _movement.IsKinematic = false;
        }
        #endregion
    }
}