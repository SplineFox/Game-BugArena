using UnityEngine;

namespace SingleUseWorld
{
    public class Bootstrap : MonoBehaviour, ICoroutineRunner
    {
        #region LifeCycle Methods
        private void Awake()
        {
            var diContainer = new DIContainer();
            var sceneLoader = new SceneLoader(this);

            diContainer.Register<ICoroutineRunner>(this);
            diContainer.Register<ITickableManager>(gameObject.AddComponent<TickableManager>());

            var gameStateMachine = new GameStateMachine(sceneLoader, diContainer);
            gameStateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
        #endregion

        #region Private Methods
        #endregion
    }
}