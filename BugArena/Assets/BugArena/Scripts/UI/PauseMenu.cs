using UnityEngine;
using UnityEngine.UI;

namespace BugArena
{
    public class PauseMenu : BaseMenu
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _quitButton;

        private IPauseService _pauseService;

        public void OnCreate(IInputService inputService, IPauseService pauseService)
        {
            base.OnCreate(inputService);
            _pauseService = pauseService;
    }

    protected override void OnOpen()
        {
            _inputService.MenuUnPausePerformed += Close;
            _resumeButton.onClick.AddListener(Close);
            _quitButton.onClick.AddListener(Quit);
            _pauseService.Pause();
        }

        protected override void OnClose()
        {
            _inputService.MenuUnPausePerformed -= Close;
            _resumeButton.onClick.RemoveListener(Close);
            _quitButton.onClick.RemoveListener(Quit);
            _pauseService.UnPause();
        }

        private void Quit()
        {
            Application.Quit();
        }
    }
}