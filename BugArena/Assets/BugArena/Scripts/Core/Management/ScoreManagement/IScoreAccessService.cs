namespace BugArena
{
    public interface IScoreAccessService
    {
        Score Score { get; }
        HighScore HighScore { get; set; }
    }
}