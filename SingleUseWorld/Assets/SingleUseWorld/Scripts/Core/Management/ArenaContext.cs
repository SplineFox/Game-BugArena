using UnityEngine;

namespace SingleUseWorld
{
    public class ArenaContext : MonoBehaviour
    {
        [SerializeField] private LevelBoundary _levelBoundary;
        [SerializeField] private ArenaCamera _arenaCamera;

        [SerializeField] private Transform _playerContainer;
        [SerializeField] private Transform _enemyContainer;
        [SerializeField] private Transform _itemContainer;
        [SerializeField] private Transform _effectContainer;

        public LevelBoundary LevelBoundary { get => _levelBoundary; }
        public ArenaCamera ArenaCamera { get => _arenaCamera; }

        public Transform PlayerContainer { get => _playerContainer; }
        public Transform EnemyContainer { get => _enemyContainer; }
        public Transform ItemContainer { get => _itemContainer; }
        public Transform EffectContainer { get => _effectContainer; }
    }
}