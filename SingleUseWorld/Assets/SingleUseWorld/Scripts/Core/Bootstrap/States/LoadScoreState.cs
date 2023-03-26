using System;
using UnityEngine;

namespace SingleUseWorld
{
    public class LoadScoreState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly DIContainer _diContainer;
        private readonly SceneLoader _sceneLoader;
        private readonly SceneCurtain _sceneCurtain;

        public LoadScoreState(GameStateMachine stateMachine, DIContainer diContainer, SceneLoader sceneLoader, SceneCurtain sceneCurtain)
        {
            _stateMachine = stateMachine;
            _diContainer = diContainer;
            _sceneLoader = sceneLoader;
            _sceneCurtain = sceneCurtain;
        }

        public void Enter()
        {
            LoadScoreOrInitializeNew();
            _stateMachine.Enter<LoadArenaState>();
        }

        public void Exit()
        {
        }

        private void LoadScoreOrInitializeNew()
        {
            var scoreAccessService = _diContainer.Resolve<IScoreAccessService>();
            var saveLoadService = _diContainer.Resolve<ISaveLoadService>();

            scoreAccessService.Score = saveLoadService.LoadScore() ?? new Score();
        }
    }
}