using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "SkullProjectileFactorySO", menuName = "SingleUseWorld/Factories/Items/SkullProjectile Factory SO")]
    public class SkullProjectileFactory : MonoFactory<SkullProjectile>
    {
        [SerializeField] private SkullProjectileSettings _settings;

        protected override void OnAfterCreate(SkullProjectile instance)
        {
            instance.Initialize(this, _settings);
        }

        protected override void OnBeforeDestroy(SkullProjectile instance)
        {
            instance.Deinitialize();
        }
    }
}
