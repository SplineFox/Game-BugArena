using UnityEngine;

namespace SingleUseWorld
{
    [CreateAssetMenu(fileName = "EffectFactorySO", menuName = "SingleUseWorld/Factories/Effects/Effect Factory SO")]
    public class EffectFactory : ScriptableFactory, IMonoFactory<Effect>
    {
        #region Fields
        [SerializeField] private Effect _effectPrefab;
        [SerializeField] private EffectType _effectType;
        #endregion

        #region Public Methods
        public Effect Create()
        {
            var effect = CreateInstance<Effect>(_effectPrefab);
            effect.OnCreate(_effectType);
            return effect;
        }
        #endregion
    }
}
