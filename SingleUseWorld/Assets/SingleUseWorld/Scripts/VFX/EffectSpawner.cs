using System.Collections.Generic;
using UnityEngine;

namespace SingleUseWorld
{
    public class EffectSpawner
    {
        #region Fields
        private EffectPool _effectPool;
        private List<Effect> _effects;
        private EffectAppearanceBuilder _appearance;
        #endregion

        #region Constructors
        public EffectSpawner(EffectPool effectPool)
        {
            _effectPool = effectPool;
            _effects = new List<Effect>();
            _appearance = new EffectAppearanceBuilder();
        }
        #endregion

        #region Public Methods
        public void SpawnEffect(EffectType effectType, Vector3 position)
        {
            var effect = _effectPool.Get(effectType);
            var effectAppearance = GetAppearance(effectType, position);
            effect.OnSpawned(this, effectAppearance);
            _effects.Add(effect);
        }

        private void SpawnEffect(EffectType effectType, EffectAppearance effectAppearance)
        {
            var effect = _effectPool.Get(effectType);
            effect.OnSpawned(this, effectAppearance);
            _effects.Add(effect);
        }

        public void SpawnEffect(ComplexEffectType complexEffectType, Vector3 position)
        {
            switch (complexEffectType)
            {
                case ComplexEffectType.Explosion:
                    SpawnExplosion(position);
                    break;
            }
        }

        public void DespawnEffect(Effect effect)
        {
            effect.OnDespawned();
            _effects.Remove(effect);
            _effectPool.Release(effect);
        }

        private void DespawnAllEffects()
        {
            for (int index = _effects.Count - 1; index >= 0; index--)
            {
                var enemy = _effects[index];
                DespawnEffect(enemy);
            }
        }
        #endregion

        #region Private Methods
        private void SpawnExplosion(Vector3 position)
        {
            _appearance.Reset();
            _appearance.WithPosition(position)
                       .WithRandomRotation()
                       .WithRandomVelocity(Vector3.right, 1.25f);
            SpawnEffect(EffectType.Smoke, _appearance.Build());

            for (int i = 0; i < 3; i++)
            {
                SpawnEffect(EffectType.Smoke, position);
            }
            SpawnEffect(EffectType.Blast, position);
        }

        private EffectAppearance GetAppearance(EffectType effectType, Vector3 position)
        {
            _appearance.Reset();
            _appearance.WithPosition(position);
            switch (effectType)
            {
                case EffectType.StepDust:
                case EffectType.PoofDust:
                    _appearance.WithRandomRotation()
                               .WithRandomVelocity(Vector3.right, 1f);
                    break;
                case EffectType.Smoke:
                    _appearance.WithRandomOffset(2f)
                               .WithRandomRotation()
                               .WithRandomVelocity(Vector3.right, 1.25f)
                               .WithRandomPlaybackTime(0.4f);
                    break;
                case EffectType.Blast:
                    _appearance.WithRandomRotation();
                    break;
            }
            return _appearance.Build();
        }
        #endregion
    }
}