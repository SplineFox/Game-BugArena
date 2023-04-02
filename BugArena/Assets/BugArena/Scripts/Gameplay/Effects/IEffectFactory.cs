namespace BugArena
{
    public interface IEffectFactory
    {
        Effect Create(EffectType effectType);
    }
}