using UnityEngine;

namespace SingleUseWorld
{
    public class SwordEntityFactory : IFactory<ItemEntity>
    {
        private SwordEntity _swordEntityPrefab;
        private SwordEntitySettings _swordEntitySettings;

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
            var swordEntity = Object.Instantiate(_swordEntityPrefab);
            swordEntity.OnCreate(_swordEntitySettings, _score, _hitTimer, _cameraShaker);
            return swordEntity;
        }
        #endregion
    }
}
