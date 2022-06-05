using UnityEngine;

namespace SingleUseWorld
{
    [RequireComponent(typeof(ProjectileMovement2D))]
    public class BaseCharacter : BaseProjectile
    {
        #region Fields
        protected ProjectileMovement2D _movement = default;
        #endregion

        #region Public Methods
        public override void Initialize()
        {
            base.Initialize();
            _movement = GetComponent<ProjectileMovement2D>();
            _movement.Initialize();
        }
        #endregion
    }
}