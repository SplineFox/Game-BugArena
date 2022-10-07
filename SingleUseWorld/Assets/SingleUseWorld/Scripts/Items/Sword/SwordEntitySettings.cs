using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "SwordEntitySettingsSO", menuName = "SingleUseWorld/Settings/Items/SwordEntity Settings SO")]
    public class SwordEntitySettings : EntitySettings
    {
        #region Fields
        [ColoredHeader("Sword parameters:", "#FD6D40")]
        [Min(1)]
        public float RotationAngle = 45f;
        [Min(0)]
        public float RotationDuration = 1f;
        #endregion
    }
}