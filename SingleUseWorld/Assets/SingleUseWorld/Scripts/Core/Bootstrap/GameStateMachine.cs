using System;
using System.Collections.Generic;

namespace SingleUseWorld
{
    public class GameStateMachine
    {
        #region Fields
        private readonly DIContainer _diContainer;
        private readonly SceneLoader _sceneLoader;

        private Dictionary<Type, IState> _states;
        private IState _currentState;
        #endregion

        #region Constructors
        public GameStateMachine(SceneLoader sceneLoader, DIContainer diContainer)
        {
            _diContainer = diContainer;
            _sceneLoader = sceneLoader;

            _currentState = null;
            _states = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, _diContainer, _sceneLoader),
                [typeof(LoadScoreState)] = new LoadScoreState(this, _diContainer, _sceneLoader),
                [typeof(LoadArenaState)] = new LoadArenaState(this, _diContainer, _sceneLoader),
                [typeof(GameLoopState)] = new GameLoopState(this, _diContainer, _sceneLoader),
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