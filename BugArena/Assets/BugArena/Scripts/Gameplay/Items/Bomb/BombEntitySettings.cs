using UnityEngine;

namespace BugArena
{
    [CreateAssetMenu(fileName = "BombEntitySettingsSO", menuName = "SingleUseWorld/Settings/Items/BombEntity Settings SO")]
    public class BombEntitySettings : EntitySettings
    {
        #region Fields
        [ColoredHeader("Bomb parameters:", "#FD6D40")]
        [Min(0f)]
        public float LaunchHorizontalSpeed = 20f;
        public float LaunchVerticalSpeed = 5f;
        public float LaunchGravity = 10f;
        
        [Space]
        [Min(1)]
        public float DamageRadius = 4;
        public LayerMask DamageMask;
        #endregion
    }
}