using UnityEngine;

namespace SingleUseWorld
{
    public class SkullItem : Item
    {
        #region Fields
        private SkullItemFactory _itemFactory;
        private SkullProjectileFactory _projectileFactory;
        #endregion

        #region Public Methods
        public void Initialize(SkullItemFactory itemFactory, SkullProjectileFactory projectileFactory)
        {
            _itemFactory = itemFactory;
            _projectileFactory = projectileFactory;
        }

        public void Deinitialize()
        {}

        public override void Use(Vector2 direction)
        {
            var skullProjectile = _projectileFactory.Create();
            skullProjectile.transform.position = this.transform.position;
            skullProjectile.Launch(direction, transform.parent.gameObject);
            _itemFactory.Destroy(this);
        }
        #endregion
    }
}