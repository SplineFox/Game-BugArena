using UnityEngine;

namespace SingleUseWorld
{
    public class LoadScoreState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly DIContainer _diContainer;
        private readonly SceneLoader _sceneLoader;

        public LoadScoreState(GameStateMachine stateMachine, DIContainer diContainer, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _diContainer = diContainer;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _stateMachine.Enter<LoadArenaState>();
        }

        public void Exit()
        {
        }
    }
}