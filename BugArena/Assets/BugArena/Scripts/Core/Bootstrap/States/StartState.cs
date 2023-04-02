using System;
using UnityEngine;

namespace BugArena
{
    public class StartState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly DIContainer _diContainer;
        private readonly SceneLoader _sceneLoader;
        private readonly SceneCurtain _sceneCurtain;

        private IMenuService _menuService;

        public StartState(GameStateMachine stateMachine, DIContainer diContainer, SceneLoader sceneLoader, SceneCurtain sceneCurtain)
        {
            _stateMachine = stateMachine;
            _diContainer = diContainer;
            _sceneLoader = sceneLoader;
            _sceneCurtain = sceneCurtain;

            _menuService = _diContainer.Resolve<IMenuService>();
        }

        public void Enter()
        {
            _menuService.Open(MenuType.Main);
        }

        public void Exit()
        {
        }
    }
}