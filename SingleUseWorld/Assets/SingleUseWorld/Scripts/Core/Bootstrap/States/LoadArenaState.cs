using UnityEngine;

namespace SingleUseWorld
{
    public class LoadArenaState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly DIContainer _diContainer;
        private readonly SceneLoader _sceneLoader;

        public LoadArenaState(GameStateMachine stateMachine, DIContainer diContainer, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _diContainer = diContainer;
            _sceneLoader = sceneLoader;
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
        }
    }
}