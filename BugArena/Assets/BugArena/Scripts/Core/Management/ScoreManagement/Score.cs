using System;
using UnityEngine;

namespace BugArena
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
        public Score(int points = 0)
        {
            _points = points;
        }
        #endregion

        #region Public Methods
        public void AddPoints(int points)
        {
            if (points == 0)
                return;

            _points += points;
            Changed.Invoke();
        }

        public void Reset()
        {
            _points = 0;
        }
        #endregion
    }
}