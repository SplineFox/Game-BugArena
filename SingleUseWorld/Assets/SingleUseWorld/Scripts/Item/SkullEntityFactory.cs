using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "SkullProjectileFactorySO", menuName = "SingleUseWorld/Factories/Items/SkullProjectile Factory SO")]
    public class SkullEntityFactory : ScriptableFactory, IMonoFactory<ItemEntity>
    {
        [SerializeField] private SkullEntity _skullEntityPrefab;
        [SerializeField] private SkullEntitySettings _skullEntitySettings;

        #region Public Mehods
        public ItemEntity Create()
        {
            var skullEntity = CreateInstance<SkullEntity>(_skullEntityPrefab);
            skullEntity.OnCreate(_skullEntitySettings);
            return skullEntity;
        }
        #endregion
    }
}
