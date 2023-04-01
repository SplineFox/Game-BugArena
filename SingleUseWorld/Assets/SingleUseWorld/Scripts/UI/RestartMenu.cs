using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SingleUseWorld
{
    public class RestartMenu : BaseMenu
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _highScoreText;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _quitButton;

        private GameStateMachine _gameStateMachine;
        private IScoreAccessService _scoreAccessService;

        public void OnCreate(GameStateMachine gameStateMachine, IScoreAccessService scoreAccessService, IInputService inputService)
        {
            base.OnCreate(inputService);
            _gameStateMachine = gameStateMachine;
            _scoreAccessService = scoreAccessService;

            _scoreText.gameObject.SetActive(false);
            _highScoreText.gameObject.SetActive(false);
            _restartButton.gameObject.SetActive(false);
            _quitButton.gameObject.SetActive(false);
        }

        protected override void OnOpen()
        {
            SetResultsText();
            _restartButton.onClick.AddListener(RestartGame);
            _quitButton.onClick.AddListener(QuitGame);

            StartCoroutine(Appear());
        }

        protected override void OnClose()
        {
            _restartButton.onClick.AddListener(RestartGame);
            _quitButton.onClick.AddListener(QuitGame);
        }

        private void RestartGame()
        {
            _gameStateMachine.Enter<GameLoopState>();
            Close();
        }

        private void QuitGame()
        {
            Application.Quit();
        }

        private void SetResultsText()
        {
            SetScoreText(_scoreAccessService.Score.Points);
            SetHighScoreText(_scoreAccessService.HighScore.Points);
        }

        private void SetScoreText(int score)
        {
            _scoreText.text = $"{score}p";
        }

        private void SetHighScoreText(int highScore)
        {
            _highScoreText.text = $"* {highScore}p *";
        }

        private IEnumerator Appear()
        {
            _restartButton.interactable = false;
            _quitButton.interactable = false;

            yield return new WaitForSeconds(0.6f);
            _scoreText.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.6f);
            _highScoreText.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.8f);
            _restartButton.gameObject.SetActive(true);
            _quitButton.gameObject.SetActive(true);

            _restartButton.interactable = true;
            _quitButton.interactable = true;
        }
    }
}
