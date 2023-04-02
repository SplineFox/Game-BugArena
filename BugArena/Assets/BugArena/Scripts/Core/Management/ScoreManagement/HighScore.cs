using System;
using UnityEngine;

namespace BugArena
{
    [Serializable]
    public class HighScore
    {
        #region Fields
        private int _points = 0;
        #endregion

        #region Properties
        public int Points
        {
            get => _points;
        }
        #endregion

        #region Constructors
        public HighScore(int points = 0)
        {
            _points = points;
        }
        #endregion

        #region Public Methods
        public void Update(int points)
        {
            if(points > _points)
            {
                _points = points;
            }
        }
        #endregion
    }
}