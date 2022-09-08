using UnityEngine;

namespace SingleUseWorld
{
    [RequireComponent(typeof(ProjectileMovement2D))]
    public class BaseCharacter : BaseProjectile
    {
        #region Fields
        protected ProjectileMovement2D _movement = default;
        #endregion

        #region LifeCycle Methods
        private void Awake()
        {
            _movement = GetComponent<ProjectileMovement2D>();
            _movement.Initialize();
        }
        #endregion
    }
}