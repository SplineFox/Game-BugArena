using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "BombEntitySettingsSO", menuName = "SingleUseWorld/Settings/Items/BombEntity Settings SO")]
    public class BombEntitySettings : ScriptableObject
    {
        #region Fields
        [Range(0f, 1.8f)]
        public float LaunchHeight = 1f;
        [Min(0.25f)]
        public float LaunchOffset = 0.34375f;
        [Min(0f)]
        public float LaunchHorizontalSpeed = 20f;
        public float LaunchVerticalSpeed = 5f;
        public float LaunchGravity = 10f;
        [Min(1)]
        public int Damage = 100;
        [Min(1)]
        public float DamageRadius = 4;
        public LayerMask DamageMask;
        #endregion
    }
}