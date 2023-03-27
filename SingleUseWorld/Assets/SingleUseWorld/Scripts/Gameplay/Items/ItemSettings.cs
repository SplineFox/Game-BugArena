using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "ItemSettingsSO", menuName = "SingleUseWorld/Settings/Items/Item Settings SO")]
    public class ItemSettings : ScriptableObject
    {
        #region Fields
        public float BobbingHeight = 0.0625f;
        public float BobbingSpeed = 8f;
        public float ElevationSpeed = 10f;
        public float MovementSpeed = 10f;
        #endregion
    }
}