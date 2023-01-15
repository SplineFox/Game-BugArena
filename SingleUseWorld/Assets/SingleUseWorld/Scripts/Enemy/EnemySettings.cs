using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "EnemySettingsSO", menuName = "SingleUseWorld/Settings/Enemies/Enemy Settings SO")]
    public class EnemySettings : ScriptableObject
    {
        #region Fields
        public EnemyHealth.Settings HealthSettings;
        public EnemySight.Settings SightSettings;
        public EnemyGrip.Settings GripSettings;

        public float KnockbackInitialHeight = 0.4375f;

        public float WanderSpeed = 1f;
        public float ChaseSpeed = 3f;

        public RangeFloat WanderMovingTime = new RangeFloat(0.5f, 1f);
        public RangeFloat WanderIdlingTime = new RangeFloat(0.5f, 1.5f);
        #endregion
    }
}