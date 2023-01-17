using Cinemachine;
using UnityEngine;

namespace SingleUseWorld
{
    public class Game : MonoBehaviour
    {
        #region Fields
        [SerializeField] private GameSettings _settings = default;
        [SerializeField] private Camera _camera = default;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera = default;
        [SerializeField] private BoxCollider2D _levelBoundaryCollider = default;
        [Space]
        [SerializeField] private Transform _enemyPoolContainer = default;
        [SerializeField] private Transform _itemPoolContainer = default;

        private Score _score;
        private LevelBoundary _levelBoundary;

        private Player _player;
        private PlayerController _playerController;
        private CameraController _cameraController;
        private TargetController _targetController;

        private MonoPool<Enemy> _wandererEnemyPool;
        private MonoPool<Enemy> _chaserEnemyPool;
        private MonoPool<Enemy> _exploderEnemyPool;

        private MonoPool<Item> _skullItemPool;
        private MonoPool<Item> _bowItemPool;
        private MonoPool<Item> _bombItemPool;
        private MonoPool<Item> _swordItemPool;

        private EnemyPool _enemyPool;
        private ItemPool _itemPool;

        private EnemySpawner _enemySpawner;
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

            // Controllers
            _player = _settings.PlayerFactory.Create();
            _playerController = new PlayerController();
            _cameraController = new CameraController(_camera, _virtualCamera);
            _targetController = new TargetController(_player.transform, _cameraController);
            _playerController.Initialize(_settings.PlayerInput, _player, _cameraController, _targetController);

            // Low-level Enemy pools
            _wandererEnemyPool = new MonoPool<Enemy>(_settings.EnemyFactory, _enemyPoolContainer, _settings.WandererEnemyPoolSettings);
            _chaserEnemyPool = new MonoPool<Enemy>(_settings.EnemyFactory, _enemyPoolContainer, _settings.ChaserEnemyPoolSettings);
            _exploderEnemyPool = new MonoPool<Enemy>(_settings.EnemyFactory, _enemyPoolContainer, _settings.ExploderEnemyPoolSettings);
            
            // Low-level Item pools
            _skullItemPool = new MonoPool<Item>(_settings.SkullItemFactory, _itemPoolContainer, _settings.SkullItemPoolSettings);
            _bowItemPool = new MonoPool<Item>(_settings.BowItemFactory, _itemPoolContainer, _settings.BowItemPoolSettings);
            _bombItemPool = new MonoPool<Item>(_settings.BombItemFactory, _itemPoolContainer, _settings.BombItemPoolSettings);
            _swordItemPool = new MonoPool<Item>(_settings.SwordItemFactory, _itemPoolContainer, _settings.SwordItemPoolSettings);

            // High-level pools
            _enemyPool = new EnemyPool(_wandererEnemyPool, _chaserEnemyPool, _exploderEnemyPool);
            _itemPool = new ItemPool(_skullItemPool, _bowItemPool, _bombItemPool, _swordItemPool);
            
            // Spawners
            _enemySpawner = new EnemySpawner(_settings.EnemySpawnerSettings, _score, _levelBoundary, _enemyPool, _player);
            _itemSpawner = new ItemSpawner(_settings.ItemSpawnerSettings, _score, _levelBoundary, _itemPool, _player);
        }

        private void Deinitialize()
        { }
        #endregion
    }
}