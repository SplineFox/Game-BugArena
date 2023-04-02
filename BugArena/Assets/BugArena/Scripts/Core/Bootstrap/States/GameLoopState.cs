using UnityEngine;

namespace BugArena
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly DIContainer _diContainer;
        private readonly SceneLoader _sceneLoader;
        private readonly SceneCurtain _sceneCurtain;

        private IArenaAccessService _arenaAccessService;
        private IInputService _inputService;
        private IMenuService _menuService;
        private ISaveLoadService _saveLoadService;
        private IScoreAccessService _scoreAccessService;

        private Vector2 _curtainScreenPoint;

        public GameLoopState(GameStateMachine stateMachine, DIContainer diContainer, SceneLoader sceneLoader, SceneCurtain sceneCurtain)
        {
            _stateMachine = stateMachine;
            _diContainer = diContainer;
            _sceneLoader = sceneLoader;
            _sceneCurtain = sceneCurtain;

            _arenaAccessService = _diContainer.Resolve<IArenaAccessService>();
            _inputService = _diContainer.Resolve<IInputService>();
            _menuService = _diContainer.Resolve<IMenuService>();

            _saveLoadService = _diContainer.Resolve<ISaveLoadService>();
            _scoreAccessService = _diContainer.Resolve<IScoreAccessService>();
        }

        public void Enter()
        {
            _inputService.PausePerformed += OnPause;
            _arenaAccessService.PlayerSpawner.PlayerDied += OnPlayerDied;
            _arenaAccessService.PlayerSpawner.PlayerDespawned += OnPlayerDespawned;

            _arenaAccessService.Arena.Populate();
            _scoreAccessService.Score.Reset();
            _arenaAccessService.ScoreCounter.ResetCounter();

            _sceneCurtain.Open();
        }

        public void Exit()
        {
            _inputService.PausePerformed -= OnPause;
            _arenaAccessService.PlayerSpawner.PlayerDied -= OnPlayerDied;
            _arenaAccessService.PlayerSpawner.PlayerDespawned -= OnPlayerDespawned;
        }

        private void OnPlayerDied()
        {
            _inputService.SwitchTo(InputType.None);
        }

        private void OnPlayerDespawned()
        {
            _sceneCurtain.Close(OnCurtainClosed);
        }

        private void OnCurtainClosed()
        {
            _arenaAccessService.Arena.Depopulate();

            _scoreAccessService.HighScore.Update(_scoreAccessService.Score.Points);
            _saveLoadService.SaveHightScore();
            _stateMachine.Enter<RestartState>();
        }

        private void OnPause()
        {
            _menuService.Open(MenuType.Pause);
        }
    }
}