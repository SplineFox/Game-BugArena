using UnityEngine;

namespace BugArena
{
    [CreateAssetMenu(fileName = "ItemTypeSettingsSO", menuName = "SingleUseWorld/Settings/Items/Item Type Settings SO")]
    public class ItemTypeSettings : ScriptableObject
    {
        #region Fields
        public ItemType Type;
        public float SpeedFactor = 0.8f;
        #endregion
    }
}