using UnityEngine;

namespace BugArena
{
    [CreateAssetMenu(fileName = "ArrowEntitySettingsSO", menuName = "SingleUseWorld/Settings/Items/ArrowEntity Settings SO")]
    public class ArrowEntitySettings : EntitySettings
    {
        #region Fields
        [ColoredHeader("Arrow parameters:", "#FD6D40")]
        [Min(0f)]
        public float LaunchSpeed = 20f;
        #endregion
    }
}