using UnityEngine;

namespace SingleUseWorld
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly DIContainer _diContainer;
        private readonly SceneLoader _sceneLoader;
        private readonly SceneCurtain _sceneCurtain;

        public GameLoopState(GameStateMachine stateMachine, DIContainer diContainer, SceneLoader sceneLoader, SceneCurtain sceneCurtain)
        {
            _stateMachine = stateMachine;
            _diContainer = diContainer;
            _sceneLoader = sceneLoader;
            _sceneCurtain = sceneCurtain;
        }

        public void Enter()
        {
            _sceneCurtain.Open(new Vector2(640, 360));
            _arena.Populate();
        }

        public void Exit()
        {
            _sceneCurtain.Close(new Vector2(640, 360));
            _arena.Depopulate();
        }
    }
}