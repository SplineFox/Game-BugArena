using System;
using UnityEngine;

namespace BugArena
{
    public class BaseComponent<T> : MonoBehaviour where T : Enum
    {
        #region Fields
        protected T _state = default;
        #endregion

        #region Properties
        public T State { get => _state; }
        #endregion

        #region Delegates & Events
        public Action<T> StateChanged = delegate { };
        #endregion

        #region Private Methods
        protected void SetState(T state)
        {
            if (_state.Equals(state))
                return;

            _state = state;
            StateChanged.Invoke(state);
        }
        #endregion
    }
}