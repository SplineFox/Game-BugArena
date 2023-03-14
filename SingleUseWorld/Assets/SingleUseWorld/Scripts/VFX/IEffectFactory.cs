namespace SingleUseWorld
{
    public interface IEffectFactory
    {
        Effect Create(EffectType effectType);
    }
}