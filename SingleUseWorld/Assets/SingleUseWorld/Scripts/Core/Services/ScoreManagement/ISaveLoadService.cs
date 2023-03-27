using UnityEngine;

namespace SingleUseWorld
{
    public interface ISaveLoadService
    {
        public void SaveHightScore();
        public HighScore LoadHighScore();
    }
}