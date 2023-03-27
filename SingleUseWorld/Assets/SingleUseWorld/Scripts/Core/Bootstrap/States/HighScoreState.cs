using System;
using UnityEngine;

namespace SingleUseWorld
{
    public class HighScoreState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly DIContainer _diContainer;
        private readonly SceneLoader _sceneLoader;
        private readonly SceneCurtain _sceneCurtain;

        public HighScoreState(GameStateMachine stateMachine, DIContainer diContainer, SceneLoader sceneLoader, SceneCurtain sceneCurtain)
        {
            _stateMachine = stateMachine;
            _diContainer = diContainer;
            _sceneLoader = sceneLoader;
            _sceneCurtain = sceneCurtain;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}