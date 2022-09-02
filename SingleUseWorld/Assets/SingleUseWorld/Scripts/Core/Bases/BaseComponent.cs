using System;
using UnityEngine;

namespace SingleUseWorld
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

        #region Public Methods
        public virtual void Initialize()
        { }
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