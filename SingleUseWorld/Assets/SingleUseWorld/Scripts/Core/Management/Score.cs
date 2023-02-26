using System;
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
        public event Action Changed = delegate { };
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
            Changed.Invoke();
        }
        #endregion

        #region Private Methods
        #endregion
    }
}