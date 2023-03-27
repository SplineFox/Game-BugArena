using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "ArenaSettings", menuName = "SingleUseWorld/Settings/Arena/Arena Settings")]
    public class ArenaSettings : ScriptableObject
    {
        #region Fields
        [Space]
        [Header("Enemy pools")]
        public MonoPoolSettings WandererEnemyPoolSettings;
        public MonoPoolSettings ChaserEnemyPoolSettings;
        public MonoPoolSettings ExploderEnemyPoolSettings;
        [Space]
        [Header("Item pools")]
        public MonoPoolSettings SkullItemPoolSettings;
        public MonoPoolSettings BowItemPoolSettings;
        public MonoPoolSettings BombItemPoolSettings;
        public MonoPoolSettings SwordItemPoolSettings;
        [Space]
        [Header("Spawners")]
        public EnemySpawner.Settings EnemySpawnerSettings;
        public ItemSpawner.Settings ItemSpawnerSettings;
        [Space]
        [Header("Core")]
        public Difficulty.Settings DifficultySettings;
        #endregion
    }
}