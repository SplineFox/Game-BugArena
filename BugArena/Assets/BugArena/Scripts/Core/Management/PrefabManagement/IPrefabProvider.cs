using UnityEngine;

namespace BugArena
{
    public interface IPrefabProvider
    {
        TPrefab Load<TPrefab>(string prefabPath) where TPrefab : Object;
    }
}