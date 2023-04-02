using UnityEngine;

namespace BugArena
{
    public enum PhysicsLayer
    {
        KnockedCharacter,
        Player,
        Enemy,
        
        Item,
        ItemEntity,

        PlayerTrigger,
        EnemyTrigger,
        ItemEntityTrigger,

        LevelBoundary,
        WorldBoundary,
    }
}