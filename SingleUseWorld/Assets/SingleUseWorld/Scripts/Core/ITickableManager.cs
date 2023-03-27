namespace SingleUseWorld
{
    internal interface ITickableManager
    {
        void Register(params ITickable[] tickables);
        void Unregister(params ITickable[] tickables);
    }
}