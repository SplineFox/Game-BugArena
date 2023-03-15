using UnityEngine;

namespace SingleUseWorld
{
    public interface IPrefabProvider
    {
        TPrefab Load<TPrefab>(string prefabPath) where TPrefab : MonoBehaviour;
    }
}