using UnityEngine;

namespace SingleUseWorld
{
    public class EffectPool
    {
        #region Fields
        private MonoPool<Effect> _stepDustEffectPool;
        private MonoPool<Effect> _poofDustEffectPool;
        private MonoPool<Effect> _smokeEffectPool;
        private MonoPool<Effect> _blastEffectPool;
        #endregion

        #region Constructors
        public EffectPool(MonoPool<Effect> stepDustEffectPool, MonoPool<Effect> poofDustEffectPool, MonoPool<Effect> smokeEffectPool, MonoPool<Effect> blastEffectPool)
        {
            _stepDustEffectPool = stepDustEffectPool;
            _poofDustEffectPool = poofDustEffectPool;
            _smokeEffectPool = smokeEffectPool;
            _blastEffectPool = blastEffectPool;
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
                case EffectType.Smoke:
                    effect = _smokeEffectPool.Get();
                    break;
                case EffectType.Blast:
                    effect = _blastEffectPool.Get();
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
                case EffectType.Smoke:
                    _smokeEffectPool.Release(effect);
                    break;
                case EffectType.Blast:
                    _blastEffectPool.Release(effect);
                    break;
            }
        }
        #endregion
    }
}