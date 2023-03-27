using UnityEngine;

namespace SingleUseWorld
{
    public class WorldBoundary : MonoBehaviour
    {
        #region LifeCycle Methods
        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.TryGetComponent<ItemEntity>(out var itemEntity))
            {
                Destroy(itemEntity.gameObject);
            }
        }
        #endregion
    }
}