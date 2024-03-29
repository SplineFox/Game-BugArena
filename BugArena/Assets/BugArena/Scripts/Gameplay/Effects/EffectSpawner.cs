using System.Collections.Generic;
using UnityEngine;

namespace BugArena
{
    public class EffectSpawner : IEffectSpawner
    {
        #region Fields
        private IEffectFactory _effectFactory;
        private IEffectAppearanceFactory _appearanceFactory;
        private List<Effect> _effects;
        #endregion

        #region Constructors
        public EffectSpawner(IEffectFactory effectFactory, IEffectAppearanceFactory appearanceFactory)
        {
            _effectFactory = effectFactory;
            _appearanceFactory = appearanceFactory;
            _effects = new List<Effect>();
        }
        #endregion

        #region Public Methods
        public void SpawnEffect(EffectType effectType, Vector3 position)
        {
            var effect = _effectFactory.Create(effectType);
            var effectAppearance = _appearanceFactory.Create(effectType, position);

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
            Object.Destroy(effect.gameObject);
        }

        public void DespawnAllEffects()
        {
            for (int index = _effects.Count - 1; index >= 0; index--)
            {
                var enemy = _effects[index];
                DespawnEffect(enemy);
            }
        }
        #endregion

        #region Private Methods
        private void SpawnEffect(EffectType effectType, EffectAppearance effectAppearance)
        {
            var effect = _effectFactory.Create(effectType);
            effect.OnSpawned(this, effectAppearance);
            _effects.Add(effect);
        }

        private void SpawnExplosion(Vector3 position)
        {
            var appearance = _appearanceFactory.Create(ComplexEffectType.Explosion, position);
            SpawnEffect(EffectType.Smoke, appearance);

            for (int i = 0; i < 3; i++)
            {
                SpawnEffect(EffectType.Smoke, position);
            }
            SpawnEffect(EffectType.Blast, position);
        }

        #endregion
    }
}