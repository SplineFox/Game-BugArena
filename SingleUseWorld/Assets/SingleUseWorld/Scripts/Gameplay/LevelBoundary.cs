using UnityEngine;

namespace SingleUseWorld
{
    public class LevelBoundary : MonoBehaviour
    {
        #region Fields
        public BoxCollider2D BoxCollider;
        #endregion

        #region LifeCycle Methods
        private void Awake()
        {
            BoxCollider = GetComponent<BoxCollider2D>();
        }
        #endregion

        #region Public Methods
        public Vector3 GetCenter()
        {
            return BoxCollider.transform.position;
        }

        public Vector3 GetRandomPositionInside()
        {
            var position = new Vector3(
                Random.Range(BoxCollider.bounds.min.x, BoxCollider.bounds.max.x),
                Random.Range(BoxCollider.bounds.min.y, BoxCollider.bounds.max.y),
                Random.Range(BoxCollider.bounds.min.z, BoxCollider.bounds.max.z));

            return position;
        }

        public bool IsPositionInside(Vector3 position)
        {
            return BoxCollider.OverlapPoint(position);
        }
        #endregion
    }
}