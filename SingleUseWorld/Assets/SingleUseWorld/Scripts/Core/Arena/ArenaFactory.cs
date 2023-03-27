using UnityEngine;

namespace SingleUseWorld
{
    public class ArenaFactory
    {
        private readonly DIContainer _diContainer;

        private IPrefabProvider _prefabProvider;
        private IConfigProvider _configProvider;
        private ITickableManager _tickableManager;

        public ArenaFactory(DIContainer diContainer)
        {
            _diContainer = diContainer;
            _prefabProvider = _diContainer.Resolve<IPrefabProvider>();
            _configProvider = _diContainer.Resolve<IConfigProvider>();
            _tickableManager = _diContainer.Resolve<ITickableManager>();
        }

        public GameObject CreateHud()
        {
            var hudPrefab = _prefabProvider.Load<GameObject>(PrefabPath.ArenaHud);
            var hud = Object.Instantiate(hudPrefab);
            var scoreCounter = hud.GetComponentInChildren<ScoreCounter>();

            scoreCounter.Construct(_diContainer.Resolve<IScoreAccessService>().Score);
            return hud;
        }

        public PlayerController CreatePlayerController(Player player, MouseAim mouseAim, CameraTargeter cameraTargeter)
        {
            var playerInput = _diContainer.Resolve<IPlayerInput>();
            var playerController = new PlayerController(playerInput, mouseAim, cameraTargeter);
            return playerController;
        }

        public PlayerSpawner CreatePlayerSpawner(LevelBoundary levelBoundary, Transform playerContainer, Player player)
        {
            var playerFactory = _diContainer.Resolve<PlayerFactory>();
            var playerSpawner = new PlayerSpawner(levelBoundary, playerContainer, player);
            return playerSpawner;
        }

        public EnemySpawner CreateEnemySpawner(LevelBoundary levelBoundary, Transform enemyContainer, Player player)
        {
            var arenaSettings = _configProvider.Load<ArenaSettings>(ConfigPath.ArenaSettings);
            
            var enemyFactory = _diContainer.Resolve<EnemyFactory>();

            var wandererEnemyPool = new MonoPool<Enemy>(enemyFactory, enemyContainer, arenaSettings.WandererEnemyPoolSettings);
            var chaserEnemyPool = new MonoPool<Enemy>(enemyFactory, enemyContainer, arenaSettings.ChaserEnemyPoolSettings);
            var exploderEnemyPool = new MonoPool<Enemy>(enemyFactory, enemyContainer, arenaSettings.ExploderEnemyPoolSettings);
            var enemyPool = new EnemyPool(wandererEnemyPool, chaserEnemyPool, exploderEnemyPool);

            var enemySpawner = new EnemySpawner(arenaSettings.EnemySpawnerSettings, levelBoundary, enemyPool, player);
            _tickableManager.Register(enemySpawner);

            return enemySpawner;
        }

        public ItemSpawner CreateItemSpawner(LevelBoundary levelBoundary, Transform itemContainer, Player player)
        {
            var arenaSettings = _configProvider.Load<ArenaSettings>(ConfigPath.ArenaSettings);

            var skullItemFactory = _diContainer.Resolve<SkullItemFactory>();
            var bowItemFactory = _diContainer.Resolve<BowItemFactory>();
            var bombItemFactory = _diContainer.Resolve<BombItemFactory>();
            var swordItemFactory = _diContainer.Resolve<SwordItemFactory>();

            var skullItemPool = new MonoPool<Item>(skullItemFactory, itemContainer, arenaSettings.SkullItemPoolSettings);
            var bowItemPool = new MonoPool<Item>(bowItemFactory, itemContainer, arenaSettings.BowItemPoolSettings);
            var bombItemPool = new MonoPool<Item>(bombItemFactory, itemContainer, arenaSettings.BombItemPoolSettings);
            var swordItemPool = new MonoPool<Item>(swordItemFactory, itemContainer, arenaSettings.SwordItemPoolSettings);
            var itemPool = new ItemPool(skullItemPool, bowItemPool, bombItemPool, swordItemPool);

            var itemSpawner = new ItemSpawner(arenaSettings.ItemSpawnerSettings, levelBoundary, itemPool, player);
            _tickableManager.Register(itemSpawner);

            return itemSpawner;
        }
    }
}