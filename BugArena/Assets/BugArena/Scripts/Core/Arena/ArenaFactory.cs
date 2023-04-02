using UnityEngine;

namespace BugArena
{
    public class ArenaFactory
    {
        private readonly DIContainer _diContainer;

        private IInputService _inputService;
        private IPrefabProvider _prefabProvider;
        private IConfigProvider _configProvider;
        private ITickableManager _tickableManager;
        private IScoreAccessService _scoreAccessService;
        private IArenaAccessService _arenaAccessService;

        public ArenaFactory(DIContainer diContainer)
        {
            _diContainer = diContainer;
            _inputService = _diContainer.Resolve<IInputService>();
            _prefabProvider = _diContainer.Resolve<IPrefabProvider>();
            _configProvider = _diContainer.Resolve<IConfigProvider>();
            _tickableManager = _diContainer.Resolve<ITickableManager>();
            _scoreAccessService = _diContainer.Resolve<IScoreAccessService>();
            _arenaAccessService = _diContainer.Resolve<IArenaAccessService>();
        }

        public ScoreCounter CreateHud()
        {
            var hudPrefab = _prefabProvider.Load<GameObject>(PrefabPath.ArenaHud);
            var hud = Object.Instantiate(hudPrefab);
            var scoreCounter = hud.GetComponentInChildren<ScoreCounter>();

            scoreCounter.Construct(_scoreAccessService.Score);
            _arenaAccessService.ScoreCounter = scoreCounter;
            return scoreCounter;
        }

        public Arena CreateArena(ArenaContext arenaContext, Player player)
        {
            var playerSpawner = CreatePlayerSpawner(arenaContext.LevelBoundary, arenaContext.PlayerContainer, player);
            var enemySpawner = CreateEnemySpawner(arenaContext.LevelBoundary, arenaContext.EnemyContainer, player);
            var itemSpawner = CreateItemSpawner(arenaContext.LevelBoundary, arenaContext.ItemContainer, player);

            var difficulty = CreateDifficulty(enemySpawner, itemSpawner);
            var arena = new Arena(playerSpawner, enemySpawner, itemSpawner);
            _arenaAccessService.Arena = arena;
            return arena;
        }

        public PlayerController CreatePlayerController(MouseAim mouseAim, CameraTargeter cameraTargeter)
        {
            var playerController = new PlayerController(_inputService, mouseAim, cameraTargeter);
            return playerController;
        }

        private Difficulty CreateDifficulty(EnemySpawner enemySpawner, ItemSpawner itemSpawner)
        {
            var difficultySettings = _configProvider.Load<ArenaSettings>(ConfigPath.ArenaSettings).DifficultySettings;
            var difficulty = new Difficulty(difficultySettings, _scoreAccessService.Score, enemySpawner, itemSpawner);
            return difficulty;
        }

        private PlayerSpawner CreatePlayerSpawner(LevelBoundary levelBoundary, Transform playerContainer, Player player)
        {
            var playerSpawner = new PlayerSpawner(levelBoundary, playerContainer, player);
            _arenaAccessService.PlayerSpawner = playerSpawner;
            return playerSpawner;
        }

        private EnemySpawner CreateEnemySpawner(LevelBoundary levelBoundary, Transform enemyContainer, Player player)
        {
            var arenaSettings = _configProvider.Load<ArenaSettings>(ConfigPath.ArenaSettings);
            var enemyPool = CreateEnemyPool(arenaSettings, enemyContainer);

            var enemySpawner = new EnemySpawner(arenaSettings.EnemySpawnerSettings, levelBoundary, enemyPool, player);
            _tickableManager.Register(enemySpawner);

            return enemySpawner;
        }

        private ItemSpawner CreateItemSpawner(LevelBoundary levelBoundary, Transform itemContainer, Player player)
        {
            var arenaSettings = _configProvider.Load<ArenaSettings>(ConfigPath.ArenaSettings);
            var itemPool = CreateItemPool(arenaSettings, itemContainer);

            var itemSpawner = new ItemSpawner(arenaSettings.ItemSpawnerSettings, levelBoundary, itemPool, player);
            _tickableManager.Register(itemSpawner);

            return itemSpawner;
        }

        private EnemyPool CreateEnemyPool(ArenaSettings arenaSettings, Transform enemyContainer)
        {
            var enemyFactory = _diContainer.Resolve<EnemyFactory>();

            var wandererEnemyPool = new MonoPool<Enemy>(enemyFactory, enemyContainer, arenaSettings.WandererEnemyPoolSettings);
            var chaserEnemyPool = new MonoPool<Enemy>(enemyFactory, enemyContainer, arenaSettings.ChaserEnemyPoolSettings);
            var exploderEnemyPool = new MonoPool<Enemy>(enemyFactory, enemyContainer, arenaSettings.ExploderEnemyPoolSettings);

            var enemyPool = new EnemyPool(wandererEnemyPool, chaserEnemyPool, exploderEnemyPool);
            return enemyPool;
        }

        private ItemPool CreateItemPool(ArenaSettings arenaSettings, Transform itemContainer)
        {
            var skullItemFactory = _diContainer.Resolve<SkullItemFactory>();
            var bowItemFactory = _diContainer.Resolve<BowItemFactory>();
            var bombItemFactory = _diContainer.Resolve<BombItemFactory>();
            var swordItemFactory = _diContainer.Resolve<SwordItemFactory>();

            var skullItemPool = new MonoPool<Item>(skullItemFactory, itemContainer, arenaSettings.SkullItemPoolSettings);
            var bowItemPool = new MonoPool<Item>(bowItemFactory, itemContainer, arenaSettings.BowItemPoolSettings);
            var bombItemPool = new MonoPool<Item>(bombItemFactory, itemContainer, arenaSettings.BombItemPoolSettings);
            var swordItemPool = new MonoPool<Item>(swordItemFactory, itemContainer, arenaSettings.SwordItemPoolSettings);

            var itemPool = new ItemPool(skullItemPool, bowItemPool, bombItemPool, swordItemPool);
            return itemPool;
        }
    }
}