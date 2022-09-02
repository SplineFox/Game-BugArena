using UnityEngine;

namespace SingleUseWorld
{
    [RequireComponent(typeof(Elevator))]
    public class BaseBehaviour : MonoBehaviour
    {
        #region Fields
        private Elevator _elevator = default;
        #endregion

        #region Properties
        public Elevator elevator { get => _elevator; }
        #endregion

        public virtual void Initialize()
        {
            _elevator = GetComponent<Elevator>();
        }
    }
}