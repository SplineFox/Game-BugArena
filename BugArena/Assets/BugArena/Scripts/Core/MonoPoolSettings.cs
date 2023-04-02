using UnityEngine;
using System;

namespace BugArena
{
    public enum ExpandMethod
    {
        Disabled,
        OneAtATime,
        Doubling
    }

    [Serializable]
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
