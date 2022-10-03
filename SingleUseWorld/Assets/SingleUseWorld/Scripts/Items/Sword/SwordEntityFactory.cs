using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "SwordEntityFactorySO", menuName = "SingleUseWorld/Factories/Items/SwordEntity Factory SO")]
    public class SwordEntityFactory : ScriptableFactory, IMonoFactory<ItemEntity>
    {
        [SerializeField] private SwordEntity _swordEntityPrefab;
        [SerializeField] private SwordEntitySettings _swordEntitySettings;

        #region Public Mehods
        public ItemEntity Create()
        {
            var swordEntity = CreateInstance<SwordEntity>(_swordEntityPrefab);
            swordEntity.OnCreate(_swordEntitySettings);
            return swordEntity;
        }
        #endregion
    }
}
