using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "ArrowEntityFactorySO", menuName = "SingleUseWorld/Factories/Items/ArrowEntity Factory SO")]
    public class ArrowEntityFactory : ScriptableFactory, IMonoFactory<ItemEntity>
    {
        [SerializeField] private ArrowEntity _arowEntityPrefab;
        [SerializeField] private ArrowEntitySettings _arowEntitySettings;

        #region Public Mehods
        public ItemEntity Create()
        {
            var arrowEntity = CreateInstance<ArrowEntity>(_arowEntityPrefab);
            arrowEntity.OnCreate(_arowEntitySettings);
            return arrowEntity;
        }
        #endregion
    }
}
