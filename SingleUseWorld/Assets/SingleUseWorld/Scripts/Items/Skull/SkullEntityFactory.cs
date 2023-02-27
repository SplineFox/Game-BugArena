﻿using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "SkullEntityFactorySO", menuName = "SingleUseWorld/Factories/Items/SkullEntity Factory SO")]
    public class SkullEntityFactory : ScriptableFactory, IMonoFactory<ItemEntity>
    {
        [SerializeField] private SkullEntity _skullEntityPrefab;
        [SerializeField] private SkullEntitySettings _skullEntitySettings;

        private Score _score;

        #region Public Mehods
        public void Inject(Score score)
        {
            _score = score;
        }

        public ItemEntity Create()
        {
            var skullEntity = CreateInstance<SkullEntity>(_skullEntityPrefab);
            skullEntity.OnCreate(_skullEntitySettings, _score);
            return skullEntity;
        }
        #endregion
    }
}
