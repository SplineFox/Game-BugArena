using UnityEngine;

namespace SingleUseWorld
{
    public class ScoreAccessService : IScoreAccessService
    {
        public Score Score { get; private set; }
        public HighScore HighScore { get; set; }

        public ScoreAccessService()
        {
            Score = new Score();
        }
    }
}