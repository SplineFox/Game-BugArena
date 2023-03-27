using UnityEngine;

namespace SingleUseWorld
{
    public class ConfigProvider : IConfigProvider
    {
        public TConfig Load<TConfig>(string configPath) where TConfig : ScriptableObject
        {
            var config = Resources.Load<TConfig>(configPath);
            return config;
        }
    }
}