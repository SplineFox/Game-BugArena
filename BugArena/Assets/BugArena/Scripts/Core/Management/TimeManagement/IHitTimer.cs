namespace BugArena
{
    public interface IHitTimer : IPausable
    {
        void StopTime(float duration);
        void ResumeTime();
    }
}