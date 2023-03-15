using UnityEngine;

namespace SingleUseWorld
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly DIContainer _diContainer;
        private readonly SceneLoader _sceneLoader;

        public GameLoopState(GameStateMachine stateMachine, DIContainer diContainer, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _diContainer = diContainer;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}