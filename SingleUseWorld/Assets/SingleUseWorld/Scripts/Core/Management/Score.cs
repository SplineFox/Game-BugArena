using UnityEngine;

namespace SingleUseWorld
{
    public class Score
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

        #region Delegates & Events
        #endregion

        #region Constructors
        public Score(int initialPoints)
        {
            _points = initialPoints;
        }
        #endregion

        #region Public Methods
        public void AddPoints(int points)
        {
            _points += points;
        }
        #endregion

        #region Private Methods
        #endregion
    }
}