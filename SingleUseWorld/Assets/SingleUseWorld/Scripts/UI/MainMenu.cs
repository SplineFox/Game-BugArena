using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SingleUseWorld
{
    public class MainMenu : BaseMenu
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _quitButton;

        private GameStateMachine _gameStateMachine;

        public void OnCreate(GameStateMachine gameStateMachine, IInputService inputService)
        {
            base.OnCreate(inputService);
            _gameStateMachine = gameStateMachine;
        }

        protected override void OnOpen()
        {
            _startButton.onClick.AddListener(StartGame);
            _quitButton.onClick.AddListener(QuitGame);
        }

        protected override void OnClose()
        {
            _startButton.onClick.AddListener(StartGame);
            _quitButton.onClick.AddListener(QuitGame);
        }

        private void StartGame()
        {
            _gameStateMachine.Enter<GameLoopState>();
            Close();
        }

        private void QuitGame()
        {
            Application.Quit();
        }
    }
}
