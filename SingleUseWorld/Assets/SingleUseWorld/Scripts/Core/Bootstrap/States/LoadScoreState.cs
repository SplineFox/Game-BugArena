using System;
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