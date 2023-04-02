using UnityEngine;

namespace BugArena
{
    public interface IConfigProvider
    {
        TConfig Load<TConfig>(string configPath) where TConfig : ScriptableObject;
    }
}