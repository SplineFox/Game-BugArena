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
        private IMenuFactory _menuFactory;
        private IArenaAccessService _arenaAccessService;

        public LoadArenaState(GameStateMachine stateMachine, DIContainer diContainer, SceneLoader sceneLoader, SceneCurtain sceneCurtain)
        {
            _stateMachine = stateMachine;
            _diContainer = diContainer;
            _sceneLoader = sceneLoader;
            _sceneCurtain = sceneCurtain;

            _arenaFactory = new ArenaFactory(_diContainer);
            _menuFactory = _diContainer.Resolve<IMenuFactory>();
            _arenaAccessService = _diContainer.Resolve<IArenaAccessService>();
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
            _stateMachine.Enter<StartState>();
        }

        private ArenaContext FindArenaContext()
        {
            var arenaContext = GameObject.FindObjectOfType<ArenaContext>();
            if (arenaContext == null)
                throw new NullReferenceException($"Failed to find object of type \"{typeof(ArenaContext)}\"");

            return arenaContext;
        }

        private void InitializeArena()
        {
            var arenaContext = FindArenaContext();
            var arenaCamera = arenaContext.ArenaCamera;

            var player = _diContainer.Resolve<PlayerFactory>().Create();
            _arenaFactory.CreateArena(arenaContext, player);

            InitializeUI();
            InitializeController(arenaCamera, player);
            InitializeVisualFeedback(arenaCamera.Shaker);
        }

        private void InitializeUI()
        {
            _arenaFactory.CreateHud();
            _menuFactory.CreateMenuRoot();
        }

        private void InitializeVisualFeedback(CameraShaker cameraShaker)
        {
            var visualFeedback = _diContainer.Resolve<IVisualFeedback>();
            var hitTimer = _diContainer.Resolve<IHitTimer>();

            visualFeedback.Timer = hitTimer;
            visualFeedback.Shaker = cameraShaker;
        }

        private void InitializeController(ArenaCamera arenaCamera, Player player)
        {
            var mouseAim = new MouseAim(arenaCamera.Main);
            var playerController = _arenaFactory.CreatePlayerController(mouseAim, arenaCamera.Targeter);
            playerController.SetPlayer(player);
        }
    }
}