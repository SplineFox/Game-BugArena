using UnityEngine;

namespace BugArena
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
        public MonoPoolSettings RockItemPoolSettings;
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