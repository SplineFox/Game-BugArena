namespace BugArena
{
    public class ArenaAccessService : IArenaAccessService
    {
        public Arena Arena { get; set; }
        public PlayerSpawner PlayerSpawner { get; set; }
        public ScoreCounter ScoreCounter { get; set; }
    }
}
