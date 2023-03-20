using UnityEngine;

namespace SingleUseWorld
{
    public interface ISaveLoadService
    {
        public void SaveScore();
        public Score LoadScore();
    }
}