using UnityEngine;

namespace SingleUseWorld
{
    public interface IEffectSpawner
    {
        void SpawnEffect(EffectType effectType, Vector3 position);
        void SpawnEffect(ComplexEffectType complexEffectType, Vector3 position);
        void DespawnEffect(Effect effect);
        void DespawnAllEffects();
    }
}