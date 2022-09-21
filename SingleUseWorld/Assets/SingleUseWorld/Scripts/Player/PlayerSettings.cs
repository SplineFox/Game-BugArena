using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "PlayerSettingsSO", menuName = "SingleUseWorld/Settings/Player/Player Settings SO")]
    public class PlayerSettings : ScriptableObject
    {
        public float MovementSpeed = 4f;
    }
}