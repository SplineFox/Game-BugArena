using UnityEngine;
using UnityEngine.Assertions;

namespace SingleUseWorld
{
    public class Character : MonoBehaviour
    {
        #region Constants
        private string VIEW_NAME = "View";
        #endregion

        #region Fields
        [SerializeField] private GameObject _view;
        #endregion

        #region Properties
        public GameObject View { get => _view; }
        #endregion

        #region LifeCycle Methods
        private void Awake()
        {
            CacheComponents();
        }
        #endregion

        #region Private Methods
        private void CacheComponents()
        {
            _view = transform.Find(VIEW_NAME)?.gameObject;
            Assert.IsNotNull(_view, "\"View GameObject\" is required.");
        }
        #endregion
    }
}