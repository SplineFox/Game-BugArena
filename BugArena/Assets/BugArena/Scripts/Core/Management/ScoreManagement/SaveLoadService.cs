using UnityEngine;

namespace BugArena
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string HighScoreKey = "HighScore";

        private readonly IScoreAccessService _scoreAccessService;

        public SaveLoadService(IScoreAccessService scoreAccessService)
        {
            _scoreAccessService = scoreAccessService;
        }

        public void SaveHightScore()
        {
            HighScore highScore = _scoreAccessService.HighScore;
            PlayerPrefs.SetInt(HighScoreKey, highScore.Points);
            PlayerPrefs.Save();
        }

        public HighScore LoadHighScore()
        {
            int points = 0;
            if (PlayerPrefs.HasKey(HighScoreKey))
                points = PlayerPrefs.GetInt(HighScoreKey);

            HighScore highScore = new HighScore(points);
            return highScore;
        }
    }
}