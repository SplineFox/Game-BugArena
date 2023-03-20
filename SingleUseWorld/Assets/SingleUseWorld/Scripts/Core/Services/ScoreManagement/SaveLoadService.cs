using UnityEngine;

namespace SingleUseWorld
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ScoreKey = "Score";

        private readonly IScoreAccessService _scoreAccessService;

        public SaveLoadService(IScoreAccessService scoreAccessService)
        {
            _scoreAccessService = scoreAccessService;
        }

        public void SaveScore()
        {
            Score score = _scoreAccessService.Score;
            string json = JsonUtility.ToJson(score);
            PlayerPrefs.SetString(ScoreKey, json);
        }

        public Score LoadScore()
        {
            string json = PlayerPrefs.GetString(ScoreKey);
            if (json == null) return null;

            Score score = JsonUtility.FromJson<Score>(json);
            return score;
        }
    }
}