using UnityEngine;

namespace BugArena
{
    [RequireComponent(typeof(ProjectileMovement2D))]
    public class BaseCharacter : BaseProjectile
    {
        #region Fields
        protected ProjectileMovement2D _movement = default;
        #endregion

        #region LifeCycle Methods
        protected override void Awake()
        {
            base.Awake();
            _movement = GetComponent<ProjectileMovement2D>();
            _movement.Initialize();
        }
        #endregion
    }
}