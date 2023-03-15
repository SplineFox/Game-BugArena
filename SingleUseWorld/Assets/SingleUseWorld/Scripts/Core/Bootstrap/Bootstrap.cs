using UnityEngine;

namespace SingleUseWorld
{
    public class Bootstrap : MonoBehaviour, ICoroutineRunner
    {
        #region Fields
        private GameStateMachine _gameStateMachine;
        #endregion

        #region LifeCycle Methods
        private void Awake()
        {
            var diContainer = new DIContainer();
            var sceneLoader = new SceneLoader(this);

            _gameStateMachine = new GameStateMachine(sceneLoader, diContainer);
            _gameStateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
        #endregion
    }
}