using System;
using UnityEngine;

namespace SingleUseWorld
{
    public class LoadArenaState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly DIContainer _diContainer;
        private readonly SceneLoader _sceneLoader;
        private readonly SceneCurtain _sceneCurtain;

        private ArenaFactory _arenaFactory;

        public LoadArenaState(GameStateMachine stateMachine, DIContainer diContainer, SceneLoader sceneLoader, SceneCurtain sceneCurtain)
        {
            _stateMachine = stateMachine;
            _diContainer = diContainer;
            _sceneLoader = sceneLoader;
            _sceneCurtain = sceneCurtain;

            _arenaFactory = new ArenaFactory(_diContainer);
        }

        public void Enter()
        {
            _sceneLoader.Load(SceneName.Arena, OnArenaSceneLoaded);
        }

        public void Exit()
        {
        }

        private void OnArenaSceneLoaded()
        {
            InitializeArena();
            _stateMachine.Enter<GameLoopState>();
        }

        private void InitializeArena()
        {
            var arenaContext = FindArenaContext();
            var arenaCamera = arenaContext.ArenaCamera;
            var player = _diContainer.Resolve<PlayerFactory>().Create();
            
            _arenaFactory.CreateHud();
            InitializeVisualFeedback(arenaCamera.Shaker);
            InitializeSpawners(arenaContext, player);
            InitializeController(arenaCamera, player);
        }

        private ArenaContext FindArenaContext()
        {
            var arenaContext = GameObject.FindObjectOfType<ArenaContext>();
            if (arenaContext == null)
                throw new NullReferenceException($"Failed to find object of type \"{typeof(ArenaContext)}\"");

            return arenaContext;
        }

        private void InitializeVisualFeedback(CameraShaker cameraShaker)
        {
            var visualFeedback = _diContainer.Resolve<IVisualFeedback>();
            var hitTimer = _diContainer.Resolve<IHitTimer>();

            visualFeedback.Timer = hitTimer;
            visualFeedback.Shaker = cameraShaker;
        }

        private void InitializeSpawners(ArenaContext arenaContext, Player player)
        {
            var playerSpawner = _arenaFactory.CreatePlayerSpawner(arenaContext.LevelBoundary, arenaContext.PlayerContainer, player);
            var enemySpawner = _arenaFactory.CreateEnemySpawner(arenaContext.LevelBoundary, arenaContext.EnemyContainer, player);
            var itemSpawner = _arenaFactory.CreateItemSpawner(arenaContext.LevelBoundary, arenaContext.ItemContainer, player);

            var difficultySettings = _diContainer.Resolve<IConfigProvider>().Load<ArenaSettings>(ConfigPath.ArenaSettings).DifficultySettings;
            var score = _diContainer.Resolve<IScoreAccessService>().Score;
            var difficulty = new Difficulty(difficultySettings, score, enemySpawner, itemSpawner);
            var arena = new Arena(playerSpawner, enemySpawner, itemSpawner);
            
            var arenaAccessService = _diContainer.Resolve<IArenaAccessService>();
            arenaAccessService.Arena = arena;
        }

        private void InitializeController(ArenaCamera arenaCamera, Player player)
        {
            var mouseAim = new MouseAim(arenaCamera.Main);
            var playerController = _arenaFactory.CreatePlayerController(player, mouseAim, arenaCamera.Targeter);
            playerController.SetPlayer(player);
        }
    }
}