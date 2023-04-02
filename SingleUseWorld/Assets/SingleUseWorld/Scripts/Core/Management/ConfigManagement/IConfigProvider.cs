using UnityEngine;

namespace SingleUseWorld
{
    public interface IConfigProvider
    {
        TConfig Load<TConfig>(string configPath) where TConfig : ScriptableObject;
    }
}