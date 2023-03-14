using UnityEngine;

namespace SingleUseWorld
{
    public class SkullEntityFactory : IFactory<ItemEntity>
    {
        private SkullEntity _skullEntityPrefab;
        private SkullEntitySettings _skullEntitySettings;

        private Score _score;
        private HitTimer _hitTimer;
        private CameraShaker _cameraShaker;

        #region Public Mehods
        public void Inject(Score score, HitTimer hitTimer, CameraShaker cameraShaker)
        {
            _score = score;
            _hitTimer = hitTimer;
            _cameraShaker = cameraShaker;
        }

        public ItemEntity Create()
        {
            var skullEntity = Object.Instantiate(_skullEntityPrefab);
            skullEntity.OnCreate(_skullEntitySettings, _score, _hitTimer, _cameraShaker);
            return skullEntity;
        }
        #endregion
    }
}
