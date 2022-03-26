#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
#endif

using UnityEngine;

namespace SingleUseWorld
{
    public class Elevator : MonoBehaviour
    {
        #region Fields
        [SerializeField]
        private List<ElevatorObserver> _observers = new List<ElevatorObserver>(2);

        [SerializeField]
        [Min(0f)]
        private float _height = 0f;
        private bool _hasChanged = false;
        #endregion

        #region Properties
        public float height
        {
            get => _height;
            set
            {
                _height = Mathf.Max(value, 0f);
                _hasChanged = true;
                NotifyHeightChanged();
            }
        }

        public bool grounded
        {
            get => _height == 0f;
        }

        public bool hasChanged
        {
            get => _hasChanged;
            set => _hasChanged = value;
        }

        public Vector3 position
        {
            get => transform.position + Vector3.up * _height;
        }
        #endregion

        #region Public Methods
        public void Translate(float translation)
        {
            height += translation;
        }
        #endregion

        #region Private Methods
        public void NotifyHeightChanged()
        {
            foreach (var observer in _observers)
                observer.UpdateHeight(_height);
        }
        #endregion

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            DrawHeight();
        }

        private void DrawHeight()
        {
            if (_height != 0)
            {
                Gizmos.color = Color.green;
                GizmosExtension.DrawLineSegment(transform.position, this.position);
                Gizmos.color = Color.white;
            }
        }
#endif
    }
}