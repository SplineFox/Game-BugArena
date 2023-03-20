using System;
using UnityEngine;

namespace SingleUseWorld
{
    public class LoadArenaState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly DIContainer _diContainer;
        private readonly SceneLoader _sceneLoader;

        private ArenaFactory _arenaFactory;

        public LoadArenaState(GameStateMachine stateMachine, DIContainer diContainer, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _diContainer = diContainer;
            _sceneLoader = sceneLoader;

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
            var arenaContext = LocateArenaContext();
            var arenaCamera = arenaContext.ArenaCamera;
            InitializeVisualFeedback(arenaCamera.Shaker);

            var playerSpawner = _arenaFactory.CreatePlayerSpawner(arenaContext.LevelBoundary, arenaContext.PlayerContainer);
            var player = playerSpawner.SpawnPlayer();
            var enemySpawner = _arenaFactory.CreateEnemySpawner(arenaContext.LevelBoundary, arenaContext.EnemyContainer, player);
            var itemSpawner = _arenaFactory.CreateItemSpawner(arenaContext.LevelBoundary, arenaContext.ItemContainer, player);
            
            var arena = new Arena(playerSpawner, enemySpawner, itemSpawner);

            var mouseAim = new MouseAim(arenaCamera.Main);
        }

        private ArenaContext LocateArenaContext()
        {
            var arenaContext = GameObject.FindObjectOfType<ArenaContext>();
            if (arenaContext == null)
                throw new NullReferenceException($"Failed to find object of type \"{nameof(ArenaContext)}\"");

            return arenaContext;
        }

        private void InitializeVisualFeedback(CameraShaker cameraShaker)
        {
            var visualFeedback = _diContainer.Resolve<IVisualFeedback>();
            var hitTimer = _diContainer.Resolve<IHitTimer>();

            visualFeedback.Timer = hitTimer;
            visualFeedback.Shaker = cameraShaker;
        }
    }
}