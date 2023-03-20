using UnityEngine;

namespace SingleUseWorld
{
    public class ScoreAccessService : IScoreAccessService
    {
        public Score Score { get; set; }
    }
}