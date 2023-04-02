using System;

namespace BugArena
{
    public interface ISceneLoader
    {
        void Load(string sceneName, Action onSceneLoaded = null);
    }
}