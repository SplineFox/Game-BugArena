using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "ArrowEntitySettingsSO", menuName = "SingleUseWorld/Settings/Items/ArrowEntity Settings SO")]
    public class ArrowEntitySettings : ScriptableObject
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
        #endregion
    }
}