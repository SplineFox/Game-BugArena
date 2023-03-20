using System;
using UnityEngine;

namespace SingleUseWorld
{
    [Serializable]
    public class Score
    {
        #region Fields
        private int _points = 0;
        private int _pointsRecord = 0;
        #endregion

        #region Properties
        public int Points
        {
            get => _points;
        }

        public int Record
        {
            get => _pointsRecord;
        }
        #endregion

        #region Delegates & Events
        public event Action Changed = delegate { };
        #endregion

        #region Constructors
        public Score()
        {
            _points = 0;
            _pointsRecord = 0;
        }

        public Score(int points, int pointsRecord)
        {
            _points = points;
            _pointsRecord = pointsRecord;
        }
        #endregion

        #region Public Methods
        public void AddPoints(int points)
        {
            if (points == 0)
                return;

            _points += points;
            _pointsRecord = Mathf.Max(_points, _pointsRecord);
            Changed.Invoke();
        }
        #endregion
    }
}