using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SingleUseWorld
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string sceneName, Action onSceneLoaded = null)
        {
            _coroutineRunner.StartCoroutine(LoadSceneCoroutine(sceneName, onSceneLoaded));
        }

        private IEnumerator LoadSceneCoroutine(string sceneName, Action onSceneLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
            {
                onSceneLoaded?.Invoke();
                yield break;
            }

            AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(sceneName);
            
            while (!loadSceneOperation.isDone)
                yield return null;
            
            onSceneLoaded?.Invoke();
        }
    }
}