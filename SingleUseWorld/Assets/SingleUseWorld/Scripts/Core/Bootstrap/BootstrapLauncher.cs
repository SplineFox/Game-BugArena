using UnityEngine;

namespace SingleUseWorld
{
    public class BootstrapLauncher : MonoBehaviour
    {
        [SerializeField] private Bootstrap _bootstrapPrefab;

        private void Awake()
        {
            var bootstrap = FindObjectOfType<Bootstrap>();

            if (bootstrap != null)
                return;

            Instantiate(_bootstrapPrefab);
        }
    }
}