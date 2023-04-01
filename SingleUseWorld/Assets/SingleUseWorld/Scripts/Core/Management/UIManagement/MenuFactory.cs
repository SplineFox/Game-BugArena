using UnityEngine;

namespace SingleUseWorld
{
    public class MenuFactory : IMenuFactory
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPrefabProvider _prefabProvider;
        private readonly IInputService _inputService;
        private readonly IPauseService _pauseService;
        private readonly IScoreAccessService _scoreAccessService;
        private Transform _menuRoot;

        public MenuFactory(GameStateMachine gameStateMachine, IPrefabProvider prefabProvider, IInputService inputService,
                                                              IPauseService pauseService, IScoreAccessService scoreAccessService)
        {
            _gameStateMachine = gameStateMachine;
            _prefabProvider = prefabProvider;
            _inputService = inputService;
            _pauseService = pauseService;
            _scoreAccessService = scoreAccessService;
        }

        public void CreateMenuRoot()
        {
            var menuRootPrefab = _prefabProvider.Load<GameObject>(PrefabPath.MenuRoot);
            var menuRoot = Object.Instantiate(menuRootPrefab);
            _menuRoot = menuRoot.transform;

            CreateMenuEventSystem();
        }

        public void CreateMainMenu()
        {
            var mainMenuPrefab = _prefabProvider.Load<MainMenu>(PrefabPath.MainMenu);
            var mainMenu = Object.Instantiate(mainMenuPrefab, _menuRoot);
            mainMenu.OnCreate(_gameStateMachine, _inputService);
        }

        public void CreatePauseMenu()
        {
            var pauseMenuPrefab = _prefabProvider.Load<PauseMenu>(PrefabPath.PauseMenu);
            var pauseMenu = Object.Instantiate(pauseMenuPrefab, _menuRoot);
            pauseMenu.OnCreate(_inputService, _pauseService);
        }

        public void CreateRestartMenu()
        {
            var restartMenuPrefab = _prefabProvider.Load<RestartMenu>(PrefabPath.RestartMenu);
            var restartScoreMenu = Object.Instantiate(restartMenuPrefab, _menuRoot);
            restartScoreMenu.OnCreate(_gameStateMachine, _scoreAccessService, _inputService);
        }

        private void CreateMenuEventSystem()
        {
            var eventSystemPrefab = _prefabProvider.Load<GameObject>(PrefabPath.MenuEventSystem);
            Object.Instantiate(eventSystemPrefab, _menuRoot);
        }
    }
}