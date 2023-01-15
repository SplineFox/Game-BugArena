using Cinemachine;
using UnityEngine;

namespace SingleUseWorld
{
    public class Game : MonoBehaviour
    {
        #region Fields
        [SerializeField] private Camera _camera = default;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera = default;
        [Space]
        [SerializeField] private PlayerInput _playerInput = default;
        [SerializeField] private PlayerFactory _playerFactory = default;
        [Space]
        [SerializeField] private EnemyFactory _enemyFactory = default;
        [SerializeField] private Transform _enemyPoolContainer = default;
        [Space]
        [SerializeField] private SkullItemFactory _skullItemFactory = default;
        [SerializeField] private BowItemFactory _bowItemFactory = default;
        [SerializeField] private BombItemFactory _bombItemFactory = default;
        [SerializeField] private SwordItemFactory _swordItemFactory = default;
        [SerializeField] private Transform _itemPoolContainer = default;
        [Space]
        [SerializeField] private BoxCollider2D _levelBoundaryCollider = default;

        private Score _score;
        private LevelBoundary _levelBoundary;

        private Player _player;
        private PlayerController _playerController;
        private CameraController _cameraController;
        private TargetController _targetController;

        private MonoPool<Enemy> _wandererEnemyPool;
        private MonoPool<Enemy> _chaserEnemyPool;
        private MonoPool<Enemy> _exploderEnemyPool;
        private EnemyPool _enemyPool;
        private EnemySpawner _enemySpawner;

        private MonoPool<Item> _skullItemPool;
        private MonoPool<Item> _bowItemPool;
        private MonoPool<Item> _bombItemPool;
        private MonoPool<Item> _swordItemPool;
        private ItemPool _itemPool;
        private ItemSpawner _itemSpawner;
        #endregion

        #region LifeCycle Methods
        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            _enemySpawner.Tick();
            _itemSpawner.Tick();
        }

        private void OnDestroy()
        {
            Deinitialize();
        }
        #endregion

        #region Public Methods
        #endregion

        #region Internal Methods
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        private void Initialize()
        {
            _score = new Score(1500);
            _levelBoundary = new LevelBoundary(_levelBoundaryCollider);

            _player = _playerFactory.Create();
            _playerController = new PlayerController();
            _cameraController = new CameraController(_camera, _virtualCamera);
            _targetController = new TargetController(_player.transform, _cameraController);
            _playerController.Initialize(_playerInput, _player, _cameraController, _targetController);

            var enemyPoolSettings = new MonoPoolSettings(10, 30, ExpandMethod.Doubling);
            _wandererEnemyPool = new MonoPool<Enemy>(_enemyFactory, _enemyPoolContainer, enemyPoolSettings);
            _chaserEnemyPool = new MonoPool<Enemy>(_enemyFactory, _enemyPoolContainer, enemyPoolSettings);
            _exploderEnemyPool = new MonoPool<Enemy>(_enemyFactory, _enemyPoolContainer, enemyPoolSettings);
            _enemyPool = new EnemyPool(_wandererEnemyPool, _chaserEnemyPool, _exploderEnemyPool);

            var enemySpawnerSettings = new EnemySpawner.Settings();
            _enemySpawner = new EnemySpawner(enemySpawnerSettings, _score, _levelBoundary, _enemyPool, _player);

            var itemPoolSettings = new MonoPoolSettings(10, 30, ExpandMethod.Doubling);
            _skullItemPool = new MonoPool<Item>(_skullItemFactory, _itemPoolContainer, itemPoolSettings);
            _bowItemPool = new MonoPool<Item>(_bowItemFactory, _itemPoolContainer, itemPoolSettings);
            _bombItemPool = new MonoPool<Item>(_bombItemFactory, _itemPoolContainer, itemPoolSettings);
            _swordItemPool = new MonoPool<Item>(_swordItemFactory, _itemPoolContainer, itemPoolSettings);
            _itemPool = new ItemPool(_skullItemPool, _bowItemPool, _bombItemPool, _swordItemPool);

            var itemSpawnerSettings = new ItemSpawner.Settings();
            _itemSpawner = new ItemSpawner(itemSpawnerSettings, _score, _levelBoundary, _itemPool, _player);
        }

        private void Deinitialize()
        {
            GameObject.Destroy(_player);
        }
        #endregion
    }
}