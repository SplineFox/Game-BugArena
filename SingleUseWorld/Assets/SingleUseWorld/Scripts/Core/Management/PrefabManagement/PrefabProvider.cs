using UnityEngine;

namespace SingleUseWorld
{
    public class PrefabProvider : IPrefabProvider
    {
        public TPrefab Load<TPrefab>(string prefabPath) where TPrefab : Object
        {
            var prefab = Resources.Load<TPrefab>(prefabPath);
            return prefab;
        }
    }
}