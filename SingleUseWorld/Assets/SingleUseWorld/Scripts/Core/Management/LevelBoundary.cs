using UnityEngine;

namespace SingleUseWorld
{
    public class LevelBoundary
    {
        #region Fields
        private BoxCollider2D _boxCollider;
        #endregion

        #region Constructors
        public LevelBoundary(BoxCollider2D boxCollider)
        {
            _boxCollider = boxCollider;
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