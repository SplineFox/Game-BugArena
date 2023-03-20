using UnityEngine;

namespace SingleUseWorld
{
    public class LevelBoundary : MonoBehaviour
    {
        #region Fields
        private BoxCollider2D _boxCollider;
        #endregion

        #region LifeCycle Methods
        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
        }
        #endregion

        #region Public Methods
        public Vector3 GetCenter()
        {
            return _boxCollider.transform.position;
        }

        public Vector3 GetRandomPositionInside()
        {
            var position = new Vector3(
                Random.Range(_boxCollider.bounds.min.x, _boxCollider.bounds.max.x),
                Random.Range(_boxCollider.bounds.min.y, _boxCollider.bounds.max.y),
                Random.Range(_boxCollider.bounds.min.z, _boxCollider.bounds.max.z));

            return position;
        }

        public bool IsPositionInside(Vector3 position)
        {
            return _boxCollider.OverlapPoint(position);
        }
        #endregion
    }
}