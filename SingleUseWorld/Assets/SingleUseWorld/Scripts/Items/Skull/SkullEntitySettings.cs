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

        public float ReboundGravity = 30f;
        public RangeFloat ReboundHorizontalSpeed = new RangeFloat(1f, 3f);
        public RangeFloat ReboundVerticalSpeed = new RangeFloat(4f, 7f);

        public float ReboundRotationAngle = 180f;
        public float ReboundRotationTime = 0.6f;
        public float ReboundFadeOutTime = 0.6f;
        #endregion
    }
}