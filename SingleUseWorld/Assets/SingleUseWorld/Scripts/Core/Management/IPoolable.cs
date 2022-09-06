using UnityEngine;
using System;

namespace SingleUseWorld
{
    public interface IPoolable
    {
        public void Reset();
    }
}
