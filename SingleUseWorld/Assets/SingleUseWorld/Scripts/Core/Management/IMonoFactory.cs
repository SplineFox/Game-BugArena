using UnityEngine;

namespace SingleUseWorld
{
    public interface IMonoFactory<TPrefab> where TPrefab : MonoBehaviour
    {
        public TPrefab Create();
    }
}
