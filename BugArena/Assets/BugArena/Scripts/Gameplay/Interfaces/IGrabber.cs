using UnityEngine;

namespace BugArena
{
    public interface IGrabber
    {
        #region Properties
        public float DamagePerSecond { get; }
        public float SlowDown { get; }
        #endregion

        #region Public Methods
        void Release();
        #endregion
    }
}