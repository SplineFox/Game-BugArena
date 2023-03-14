using UnityEngine;

namespace SingleUseWorld
{
    public class ArrowEntityFactory : IFactory<ItemEntity>
    {
        private ArrowEntity _arowEntityPrefab;
        private ArrowEntitySettings _arowEntitySettings;

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
            var arrowEntity = Object.Instantiate(_arowEntityPrefab);
            arrowEntity.OnCreate(_arowEntitySettings, _score, _hitTimer, _cameraShaker);
            return arrowEntity;
        }
        #endregion
    }
}
