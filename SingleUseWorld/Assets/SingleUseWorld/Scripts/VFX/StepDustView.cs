using UnityEngine;

namespace SingleUseWorld
{
    public class StepDustView : MonoBehaviour
    {
        #region Private Methods
        private void OnAnimationEndFrame()
        {
            Destroy(this.gameObject);
        }
        #endregion
    }
}