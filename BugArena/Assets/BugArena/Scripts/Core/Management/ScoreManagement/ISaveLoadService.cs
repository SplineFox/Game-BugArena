using UnityEngine;

namespace BugArena
{
    public interface ISaveLoadService
    {
        public void SaveHightScore();
        public HighScore LoadHighScore();
    }
}