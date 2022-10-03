using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "PlayerSettingsSO", menuName = "SingleUseWorld/Settings/Player/Player Settings SO")]
    public class PlayerSettings : ScriptableObject
    {
        public PlayerSpeed.Settings SpeedSettings;
        public PlayerHealth.Settings HealthSettings;
        public PlayerArmament.Settings ArmamentSettings;
        public PlayerGripHandler.Settings GripHandlerSettings;
    }
}