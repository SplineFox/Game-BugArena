using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "BombEntityFactorySO", menuName = "SingleUseWorld/Factories/Items/BombEntity Factory SO")]
    public class BombEntityFactory : ScriptableFactory, IMonoFactory<ItemEntity>
    {
        [SerializeField] private BombEntity _bombEntityPrefab;
        [SerializeField] private BombEntitySettings _bombEntitySettings;

        #region Public Mehods
        public ItemEntity Create()
        {
            var bombEntity = CreateInstance<BombEntity>(_bombEntityPrefab);
            bombEntity.OnCreate(_bombEntitySettings);
            return bombEntity;
        }
        #endregion
    }
}
