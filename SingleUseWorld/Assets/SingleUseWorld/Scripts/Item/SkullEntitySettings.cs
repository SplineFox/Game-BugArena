using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "SkullEntitySettingsSO", menuName = "SingleUseWorld/Settings/Items/SkullEntity Settings SO")]
    public class SkullEntitySettings : ScriptableObject
    {
        #region Fields
        [Range(0f, 1.8f)]
        public float LaunchHeight = 1f;
        [Min(0f)]
        public float LaunchSpeed = 20f;
        [Min(0.25f)]
        public float LaunchOffset = 0.34375f;
        [Min(1)]
        public int Damage = 100;
        #endregion
    }
}