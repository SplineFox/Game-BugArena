using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "SkullItemFactorySO", menuName = "SingleUseWorld/Factories/Items/SkullItem Factory SO")]
    public class SkullItemFactory : MonoFactory<SkullItem>
    {
        [SerializeField] private SkullProjectileFactory _skullProjectileFactory;

        protected override void OnAfterCreate(SkullItem instance)
        {
            instance.Initialize(this, _skullProjectileFactory);
        }

        protected override void OnBeforeDestroy(SkullItem instance)
        {
            instance.Deinitialize();
        }
    }
}
