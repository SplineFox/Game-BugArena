using UnityEngine;

namespace SingleUseWorld
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
            string json = JsonUtility.ToJson(highScore);
            PlayerPrefs.SetString(HighScoreKey, json);
        }

        public HighScore LoadHighScore()
        {
            string json = PlayerPrefs.GetString(HighScoreKey);
            if (json == null) return null;

            HighScore highScore = JsonUtility.FromJson<HighScore>(json);
            return highScore;
        }
    }
}