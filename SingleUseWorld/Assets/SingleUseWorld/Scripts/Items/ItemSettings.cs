using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "ItemSettingsSO", menuName = "SingleUseWorld/Settings/Items/Item Settings SO")]
    public class ItemSettings : ScriptableObject
    {
        #region Fields
        public float SpeedFactor = 0.8f;
        #endregion
    }
}