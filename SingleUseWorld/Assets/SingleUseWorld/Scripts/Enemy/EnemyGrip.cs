using UnityEngine;

namespace SingleUseWorld
{
    public enum GripState
    {
        Grabbed,
        Released
    }

    [RequireComponent(typeof(Collider2D))]
    public class EnemyGrip : BaseComponent<GripState>
    {
        #region Fields
        private Collider2D _collider2D = default;
        private GameObject _target = default;

        private float _grabbingTension = 0.1f;
        #endregion

        #region LifeCycle Methods
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<IGrabbable>(out var grabbable))
            {
                _target = collision.gameObject;
                SetState(GripState.Grabbed);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject == _target.gameObject)
            {
                _target = null;
                SetState(GripState.Released);
            }
        }
        #endregion

        #region Public Methods
        public override void Initialize()
        {
            _collider2D = GetComponent<Collider2D>();
            _state = GripState.Released;
        }
        #endregion

        #region Private Methods
        private void Grab()
        {

        }

        private void Release()
        {

        }
        #endregion
    }
}