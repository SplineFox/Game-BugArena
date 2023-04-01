using System;
using UnityEngine;

namespace SingleUseWorld
{
    public class RestartState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly DIContainer _diContainer;
        private readonly SceneLoader _sceneLoader;
        private readonly SceneCurtain _sceneCurtain;

        private IMenuService _menuService;

        public RestartState(GameStateMachine stateMachine, DIContainer diContainer, SceneLoader sceneLoader, SceneCurtain sceneCurtain)
        {
            _stateMachine = stateMachine;
            _diContainer = diContainer;
            _sceneLoader = sceneLoader;
            _sceneCurtain = sceneCurtain;

            _menuService = _diContainer.Resolve<IMenuService>();
        }

        public void Enter()
        {
            _menuService.Open(MenuType.Restart);
        }

        public void Exit()
        {
        }
    }
}