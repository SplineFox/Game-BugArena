using System;
using UnityEngine;

namespace SingleUseWorld
{
    public enum SightState
    {
        InSight,
        OutSight
    }

    [RequireComponent(typeof(Collider2D))]
    public class EnemySight : BaseComponent<SightState>
    {
        #region Fields
        private Collider2D _collider2D = default;
        private Transform _target = default;
        #endregion

        #region Properties
        public Transform Target { get => _target; }
        public Vector2 DirectionToTarget
        {
            get
            {
                if (_target == null)
                    return Vector2.zero;

                return (_target.position - transform.position).normalized;
            }
        }
        #endregion

        #region LifeCycle Methods
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<Player>(out var player))
            {
                _target = player.transform;
                SetState(SightState.InSight);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(_target != null && _target.gameObject == collision.gameObject)
            {
                _target = null;
                SetState(SightState.OutSight);
            }
        }
        #endregion

        #region Public Methods
        public override void Initialize()
        {
            _collider2D = GetComponent<Collider2D>();
            _state = SightState.OutSight;
        }
        #endregion
    }
}