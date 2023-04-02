using System;
using UnityEngine;

namespace BugArena
{
    [RequireComponent(typeof(Collider2D))]
    public class ProjectileTrigger : MonoBehaviour
    {
        #region Fields
        private Collider2D _collider2D = default;
        #endregion

        #region Delegates & Events
        public Action<Enemy> EnemyHit = delegate { };
        #endregion

        #region LifeCycle Methods
        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.TryGetComponent<Enemy>(out var enemy))
            {
                EnemyHit.Invoke(enemy);
            }
        }
        #endregion
    }
}