using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "EnemySettingsSO", menuName = "SingleUseWorld/Settings/Enemies/Enemy Settings SO")]
    public class EnemySettings : ScriptableObject
    {
        #region Fields
        public EnemySight.Settings SightSettings;

        public float WanderSpeed = 1f;
        public float ChaseSpeed = 3f;

        public float WanderMovingTime = 1f;
        public float WanderIdlingTime = 1f;
        #endregion
    }
}