using UnityEngine;
using System;

namespace SingleUseWorld
{
    public enum ExpandMethod
    {
        Disabled,
        OneAtATime,
        Doubling
    }

    public struct MonoPoolSettings
    {
        public int InitialSize;
        public int MaxSize;
        public ExpandMethod PoolExpandMethod;

        public MonoPoolSettings(int initialSize, int maxSize, ExpandMethod poolExpandMethod)
        {
            InitialSize = initialSize;
            MaxSize = maxSize;
            PoolExpandMethod = poolExpandMethod;
        }
    }
}
