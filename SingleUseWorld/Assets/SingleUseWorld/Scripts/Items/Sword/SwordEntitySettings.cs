using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "SwordEntitySettingsSO", menuName = "SingleUseWorld/Settings/Items/SwordEntity Settings SO")]
    public class SwordEntitySettings : ScriptableObject
    {
        #region Fields
        [Range(0f, 1.8f)]
        public float LaunchHeight = 0.5f;
        [Min(0f)]
        public float LaunchSpeed = 20f;
        [Min(0.25f)]
        public float LaunchOffset = 1f;
        [Min(1)]
        public int Damage = 100;
        [Min(1)]
        public float RotationAngle = 45f;
        [Min(0)]
        public float RotationDuration = 1f;
        #endregion
    }
}