using System;
using System.Collections.Generic;

namespace BugArena
{
    public class GameStateMachine
    {
        #region Fields
        private readonly DIContainer _diContainer;
        private readonly SceneLoader _sceneLoader;
        private readonly SceneCurtain _sceneCurtain;

        private Dictionary<Type, IState> _states;
        private IState _currentState;
        #endregion

        #region Constructors
        public GameStateMachine(DIContainer diContainer, SceneLoader sceneLoader, SceneCurtain sceneCurtain)
        {
            _diContainer = diContainer;
            _sceneLoader = sceneLoader;
            _sceneCurtain = sceneCurtain;

            _currentState = null;
            _states = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)]    = new BootstrapState(this, _diContainer, _sceneLoader, _sceneCurtain),
                [typeof(LoadScoreState)]    = new LoadScoreState(this, _diContainer, _sceneLoader, _sceneCurtain),
                [typeof(LoadArenaState)]    = new LoadArenaState(this, _diContainer, _sceneLoader, _sceneCurtain),
                [typeof(StartState)]        = new StartState(this, _diContainer, _sceneLoader, _sceneCurtain),
                [typeof(GameLoopState)]     = new GameLoopState(this, _diContainer, _sceneLoader, _sceneCurtain),
                [typeof(RestartState)]      = new RestartState(this, _diContainer, _sceneLoader, _sceneCurtain),
            };
        }
        #endregion

        #region Public Methods
        public void Enter<TState>() where TState : IState
        {
            _currentState?.Exit();
            var state = _states[typeof(TState)];
            _currentState = state;
            state.Enter();
        }
        #endregion
    }
}