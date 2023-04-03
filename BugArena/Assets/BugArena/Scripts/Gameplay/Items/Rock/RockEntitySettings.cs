using UnityEngine;

namespace BugArena
{
    [CreateAssetMenu(fileName = "RockEntitySettingsSO", menuName = "SingleUseWorld/Settings/Items/RockEntity Settings SO")]
    public class RockEntitySettings : EntitySettings
    {
        #region Fields
        [ColoredHeader("Rock parameters:", "#FD6D40")]
        [Min(0f)]
        public float LaunchSpeed = 20f;

        [Space]
        public float ReboundGravityScale = 30f;
        public RangeFloat ReboundHorizontalSpeed = new RangeFloat(1f, 3f);
        public RangeFloat ReboundVerticalSpeed = new RangeFloat(4f, 7f);
        public float ReboundSpinSpeed = 90f;
        public float ReboundFadeOutTime = 0.6f;
        #endregion
    }
}