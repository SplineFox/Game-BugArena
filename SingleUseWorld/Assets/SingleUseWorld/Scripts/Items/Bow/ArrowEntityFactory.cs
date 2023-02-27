using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "ArrowEntityFactorySO", menuName = "SingleUseWorld/Factories/Items/ArrowEntity Factory SO")]
    public class ArrowEntityFactory : ScriptableFactory, IMonoFactory<ItemEntity>
    {
        [SerializeField] private ArrowEntity _arowEntityPrefab;
        [SerializeField] private ArrowEntitySettings _arowEntitySettings;

        private Score _score;

        #region Public Mehods
        public void Inject(Score score)
        {
            _score = score;
        }

        public ItemEntity Create()
        {
            var arrowEntity = CreateInstance<ArrowEntity>(_arowEntityPrefab);
            arrowEntity.OnCreate(_arowEntitySettings, _score);
            return arrowEntity;
        }
        #endregion
    }
}
