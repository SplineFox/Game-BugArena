using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "GameSettingsSO", menuName = "SingleUseWorld/Settings/Game/Game Settings SO")]
    public class GameSettings : ScriptableObject
    {
        #region Fields
        [Header("Player")]
        public PlayerFactory PlayerFactory;
        [Space]
        [Header("Enemy factories")]
        public EnemyFactory EnemyFactory;
        [Space]
        [Header("Item factories")]
        public SkullItemFactory SkullItemFactory;
        public BowItemFactory BowItemFactory;
        public BombItemFactory BombItemFactory;
        public SwordItemFactory SwordItemFactory;
        [Space]
        [Header("Entities factories")]
        public SkullEntityFactory SkullEntityFactory;
        public ArrowEntityFactory ArrowEntityFactory;
        public BombEntityFactory BombEntityFactory;
        public SwordEntityFactory SwordEntityFactory;
        [Space]
        [Header("Effect factories")]
        public EffectFactory StepDustEffectFactory;
        public EffectFactory PoofDustEffectFactory;
        public EffectFactory SmokeEffectFactory;
        public EffectFactory BlastEffectFactory;
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
        [Header("Effect pools")]
        public MonoPoolSettings StepDustEffectPool;
        public MonoPoolSettings PoofDustEffectPool;
        public MonoPoolSettings SmokeEffectPool;
        public MonoPoolSettings BlastEffectPool;
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