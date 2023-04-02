namespace SingleUseWorld
{
    public interface IPauseService
    {
        bool Paused { get; }

        void Pause();
        void UnPause();
    }
}