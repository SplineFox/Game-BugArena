using UnityEngine;

namespace SingleUseWorld
{
    [RequireComponent(typeof(Elevator), typeof (ProjectileMovement))]
    public class Projectile : MonoBehaviour
    {
        #region Fields
        private Elevator _elevator = default;
        private ProjectileMovement _movement = default;
        [SerializeField] private ProjectileView _view = default;
        #endregion

        #region LifeCycle Methods
        private void Start()
        {
            _elevator = GetComponent<Elevator>();
            _movement = GetComponent<ProjectileMovement>();
        }

        private void Update()
        {
            _view.UpdateHeightPresentation(_elevator.height);
        }
        #endregion
    }
}