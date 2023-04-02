namespace BugArena
{
    public interface IPauseService
    {
        bool Paused { get; }

        void Pause();
        void UnPause();
    }
}