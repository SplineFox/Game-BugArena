using UnityEngine;

namespace SingleUseWorld
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly DIContainer _diContainer;
        private readonly SceneLoader _sceneLoader;
        private readonly SceneCurtain _sceneCurtain;

        private IScoreAccessService _scoreAccessService;
        private ISaveLoadService _saveLoadService;
        private IArenaAccessService _arenaAccessService;

        public GameLoopState(GameStateMachine stateMachine, DIContainer diContainer, SceneLoader sceneLoader, SceneCurtain sceneCurtain)
        {
            _stateMachine = stateMachine;
            _diContainer = diContainer;
            _sceneLoader = sceneLoader;
            _sceneCurtain = sceneCurtain;

            _arenaAccessService = _diContainer.Resolve<IArenaAccessService>();
            _scoreAccessService = _diContainer.Resolve<IScoreAccessService>();
        }

        public void Enter()
        {
            _sceneCurtain.Open(new Vector2(640, 360));
            _arenaAccessService.Arena.Populate();
            _scoreAccessService.Score.Reset();
        }

        public void Exit()
        {
            _sceneCurtain.Close(new Vector2(640, 360));
            _arenaAccessService.Arena.Depopulate();
            _arenaAccessService.Arena = null;

            _saveLoadService.SaveHightScore();
            _stateMachine.Enter<HighScoreState>();
        }
    }
}