using UnityEngine;

namespace SingleUseWorld
{
    public class EffectPool
    {
        #region Fields
        private MonoPool<Effect> _stepDustEffectPool;
        private MonoPool<Effect> _poofDustEffectPool;
        #endregion

        #region Constructors
        public EffectPool(MonoPool<Effect> stepDustEffectPool, MonoPool<Effect> poofDustEffectPool)
        {
            _stepDustEffectPool = stepDustEffectPool;
            _poofDustEffectPool = poofDustEffectPool;
        }
        #endregion

        #region Public Methods
        public Effect Get(EffectType effectType)
        {
            Effect effect = default;
            switch (effectType)
            {
                case EffectType.StepDust:
                    effect = _stepDustEffectPool.Get();
                    break;
                case EffectType.PoofDust:
                    effect = _poofDustEffectPool.Get();
                    break;
                default:
                    effect = _poofDustEffectPool.Get();
                    break;
            }
            return effect;
        }

        public void Release(Effect effect)
        {
            switch (effect.Type)
            {
                case EffectType.StepDust:
                    _stepDustEffectPool.Release(effect);
                    break;
                case EffectType.PoofDust:
                    _poofDustEffectPool.Release(effect);
                    break;
            }
        }
        #endregion
    }
}