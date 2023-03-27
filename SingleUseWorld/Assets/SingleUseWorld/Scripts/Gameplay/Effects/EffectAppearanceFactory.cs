using UnityEngine;

namespace SingleUseWorld
{
    public class EffectAppearanceFactory : IEffectAppearanceFactory
    {
        private EffectAppearanceBuilder _appearance;

        public EffectAppearanceFactory()
        {
            _appearance = new EffectAppearanceBuilder();
        }

        public EffectAppearance Create(EffectType effectType, Vector3 position)
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

        public EffectAppearance Create(ComplexEffectType complexEffectType, Vector3 position)
        {
            _appearance.Reset();
            _appearance.WithPosition(position)
                       .WithRandomRotation()
                       .WithRandomVelocity(Vector3.right, 1.25f);
            return _appearance.Build();
        }
    }
}