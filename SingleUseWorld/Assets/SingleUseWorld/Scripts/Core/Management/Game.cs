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
        [SerializeField] private HitTimer _hitTimer = default;
        [SerializeField] private CameraShaker _cameraShaker = default;
        [Space]
        [SerializeField] private Transform _enemyPoolContainer = default;
        [SerializeField] private Transform _itemPoolContainer = default;
        [SerializeField] private Transform _effectPoolContainer = default;

        private Score _score;
        private Difficulty _difficulty;
        private LevelBoundary _levelBoundary;

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

        private MonoPool<Effect> _stepDustEffectPool;
        private MonoPool<Effect> _poofDustEffectPool;
        private MonoPool<Effect> _smokeEffectPool;
        private MonoPool<Effect> _blastEffectPool;

        private EnemyPool _enemyPool;
        private ItemPool _itemPool;
        private EffectPool _effectPool;

        private EnemySpawner _enemySpawner;
        private ItemSpawner _itemSpawner;
        private EffectSpawner _effectSpawner;
        private PlayerSpawner _playerSpawner;
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

            // Low-level Effect pools
            _stepDustEffectPool = new MonoPool<Effect>(_settings.StepDustEffectFactory, _effectPoolContainer, _settings.StepDustEffectPool);
            _poofDustEffectPool = new MonoPool<Effect>(_settings.PoofDustEffectFactory, _effectPoolContainer, _settings.PoofDustEffectPool);
            _smokeEffectPool = new MonoPool<Effect>(_settings.SmokeEffectFactory, _effectPoolContainer, _settings.SmokeEffectPool);
            _blastEffectPool = new MonoPool<Effect>(_settings.BlastEffectFactory, _effectPoolContainer, _settings.BlastEffectPool);

            _effectPool = new EffectPool(_stepDustEffectPool, _poofDustEffectPool, _smokeEffectPool, _blastEffectPool);
            _effectSpawner = new EffectSpawner(_effectPool);

            _settings.PlayerFactory.Inject(_effectSpawner);
            _settings.EnemyFactory.Inject(_effectSpawner);

            _settings.SkullEntityFactory.Inject(_score, _hitTimer, _cameraShaker);
            _settings.ArrowEntityFactory.Inject(_score, _hitTimer, _cameraShaker);
            _settings.SwordEntityFactory.Inject(_score, _hitTimer, _cameraShaker);
            _settings.BombEntityFactory.Inject(_score, _effectSpawner, _hitTimer, _cameraShaker);

            // Controllers
            _cameraController = new CameraController(_camera, _virtualCamera);
            _targetController = new TargetController(_cameraController);
            _playerController = new PlayerController(_settings.PlayerInput, _cameraController, _targetController);

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
            _playerSpawner = new PlayerSpawner(_levelBoundary, _settings.PlayerFactory, _playerController);
            _enemySpawner = new EnemySpawner(_settings.EnemySpawnerSettings, _levelBoundary, _enemyPool, _playerSpawner);
            _itemSpawner = new ItemSpawner(_settings.ItemSpawnerSettings, _levelBoundary, _itemPool, _playerSpawner);

            _difficulty = new Difficulty(_settings.DifficultySettings, _score, _enemySpawner, _itemSpawner);

            _playerSpawner.SpawnPlayer();
        }

        private void Deinitialize()
        { }
        #endregion
    }
}