using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace General.Canvas {
    public class CanvasController : MonoBehaviour {

        [SerializeField] private GameObject panel;
        [SerializeField] private GameObject levelEndedUI;
        [SerializeField] private GameObject scoreText;
        [SerializeField] private GameObject highScoreText;
        [SerializeField] private GameObject yourScoreText;
        [SerializeField] private GameObject pauseButton;

        private int _score;

        private void Start() {
            GameManager.Instance.OnLevelEnded += ShowLevelEndedUI;
            PlayerController.OnObstaclePassed += UpdateScoreUI;
            PlayerController.OnObstaclePassed += UpdateHighScoreUI;
            UpdateScoreUI();
            UpdateHighScoreUI();
        }

        private void OnDisable() {
            GameManager.Instance.OnLevelEnded -= ShowLevelEndedUI;
            PlayerController.OnObstaclePassed -= UpdateScoreUI;
            PlayerController.OnObstaclePassed -= UpdateHighScoreUI;
        }

        private void UpdateScoreUI() {
            _score = GameManager.Instance.CurrentScoreMap[GameManager.Instance.ActiveGameMode];
            scoreText.GetComponent<TextMeshProUGUI>().text = _score.ToString();
        }

        private void UpdateHighScoreUI() {
            int highScore = DataManager.Instance.GetHighScore(GameManager.Instance.ActiveGameMode);
            highScoreText.GetComponent<TextMeshProUGUI>().text = highScore.ToString();
        }

        private void ShowLevelEndedUI() {
            pauseButton.SetActive(false);
            levelEndedUI.SetActive(true);
            yourScoreText.SetActive(true);
        }

        public void SetPanelTransparent() {
            panel.GetComponent<Image>().color = new Color(
                0,
                0,
                0,
                0.6f
            );
        }

        public void SetPanelOpaque() {
            panel.GetComponent<Image>().color = new Color(
                0,
                0,
                0,
                0
            );
        }
    }
}