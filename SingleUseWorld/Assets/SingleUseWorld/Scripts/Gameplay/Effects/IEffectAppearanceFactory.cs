using UnityEngine;

namespace SingleUseWorld
{
    public interface IEffectAppearanceFactory
    {
        EffectAppearance Create(EffectType effectType, Vector3 position);
        EffectAppearance Create(ComplexEffectType complexEffectType, Vector3 position);
    }
}