using System;

namespace SingleUseWorld
{
    public interface ISceneLoader
    {
        void Load(string sceneName, Action onSceneLoaded = null);
    }
}