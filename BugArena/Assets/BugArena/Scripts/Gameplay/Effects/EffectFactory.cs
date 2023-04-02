using UnityEngine;

namespace BugArena
{
    public class EffectFactory : IEffectFactory
    {
        private readonly IPrefabProvider _prefabProvider;

        public EffectFactory(IPrefabProvider prefabProvider)
        {
            _prefabProvider = prefabProvider;
        }

        public Effect Create(EffectType effectType)
        {
            var effectPrefab = PrefabFor(effectType);

            var effect = Object.Instantiate(effectPrefab);
            effect.OnCreate(effectType);

            return effect;
        }

        private Effect PrefabFor(EffectType effectType)
        {
            Effect effectPrefab = null;
            switch (effectType)
            {
                case EffectType.StepDust:
                    effectPrefab = _prefabProvider.Load<Effect>(PrefabPath.StepDust);
                    break;
                case EffectType.PoofDust:
                    effectPrefab = _prefabProvider.Load<Effect>(PrefabPath.PoofDust);
                    break;
                case EffectType.Smoke:
                    effectPrefab = _prefabProvider.Load<Effect>(PrefabPath.Smoke);
                    break;
                case EffectType.Blast:
                    effectPrefab = _prefabProvider.Load<Effect>(PrefabPath.Blast);
                    break;
            }
            return effectPrefab;
        }
    }
}
