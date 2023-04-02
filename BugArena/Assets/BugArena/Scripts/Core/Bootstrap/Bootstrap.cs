using UnityEngine;

namespace BugArena
{
    public class Bootstrap : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private SceneCurtain _sceneCurtainPrefab;

        #region LifeCycle Methods
        private void Awake()
        {
            var diContainer = new DIContainer();
            var sceneLoader = new SceneLoader(this);
            var sceneCurtain = Instantiate<SceneCurtain>(_sceneCurtainPrefab);

            diContainer.Register<ICoroutineRunner>(this);
            diContainer.Register<ITickableManager>(gameObject.AddComponent<TickableManager>());

            var gameStateMachine = new GameStateMachine(diContainer, sceneLoader, sceneCurtain);
            gameStateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
            DontDestroyOnLoad(sceneCurtain);
        }
        #endregion

        #region Private Methods
        #endregion
    }
}