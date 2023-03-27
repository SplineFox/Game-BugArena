using System.Collections.Generic;
using UnityEngine;

namespace SingleUseWorld
{
    public class TickableManager : MonoBehaviour, ITickableManager
    {
        private readonly List<ITickable> _tickables = new List<ITickable>();

        public void Register(params ITickable[] tickables)
        {
            foreach (var tickable in tickables)
                if (!_tickables.Contains(tickable))
                    _tickables.Add(tickable);
        }

        public void Unregister(params ITickable[] tickables)
        {
            foreach (var tickable in tickables)
                if (_tickables.Contains(tickable))
                    _tickables.Remove(tickable);
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            foreach (var tickable in _tickables)
            {
                tickable.Tick(deltaTime);
            }
        }
    }
}