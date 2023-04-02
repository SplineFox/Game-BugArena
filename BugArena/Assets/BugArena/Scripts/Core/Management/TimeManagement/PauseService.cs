using System.Collections.Generic;
using UnityEngine;

namespace BugArena
{
    public class PauseService : IPauseService
    {
        private bool _isPaused;
        private List<IPausable> _pausables;

        public bool Paused
        {
            get => _isPaused;
        }

        public PauseService()
        {
            _isPaused = false;
            _pausables = new List<IPausable>();
        }

        public void Register(params IPausable[] pausables)
        {
            foreach (var pausable in pausables)
                if (!_pausables.Contains(pausable))
                    _pausables.Add(pausable);
        }

        public void UnRegister(params IPausable[] pausables)
        {
            foreach (var pausable in pausables)
                if (_pausables.Contains(pausable))
                    _pausables.Remove(pausable);
        }

        public void Pause()
        {
            if (_isPaused)
                return;

            Time.timeScale = 0f;
            _isPaused = true;
            NotifyPaused();
        }

        public void UnPause()
        {
            if (!_isPaused)
                return;

            Time.timeScale = 1f;
            _isPaused = false;
            NotifyUnPaused();
        }

        private void NotifyPaused()
        {
            foreach (var pauseable in _pausables)
            {
                pauseable.Pause();
            }
        }

        private void NotifyUnPaused()
        {
            foreach (var pauseable in _pausables)
            {
                pauseable.UnPause();
            }
        }

    }
}