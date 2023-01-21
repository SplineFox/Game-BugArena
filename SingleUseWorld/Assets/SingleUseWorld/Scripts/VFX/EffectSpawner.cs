using System.Collections.Generic;
using UnityEngine;

namespace SingleUseWorld
{
    public class EffectSpawner
    {
        #region Fields
        private EffectPool _effectPool;
        private List<Effect> _effects;
        #endregion

        #region Constructors
        public EffectSpawner(EffectPool effectPool)
        {
            _effectPool = effectPool;
            _effects = new List<Effect>();
        }
        #endregion

        #region Public Methods
        public void SpawnEffect(EffectType effectType, Vector3 position)
        {
            var effect = _effectPool.Get(effectType);
            effect.OnSpawned(position, this);
            _effects.Add(effect);
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
    }
}