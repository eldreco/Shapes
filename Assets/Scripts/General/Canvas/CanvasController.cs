using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public static CanvasController Instance;

    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject levelEndedUI;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private GameObject highScoreText;
    [SerializeField] private GameObject yourScoreText;
    [SerializeField] private GameObject pauseButton;

    private int score;

    private void Awake() {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

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

    private void UpdateScoreUI(){
        score = GameManager.Instance.Score;
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString();
    }

    private void UpdateHighScoreUI(){
        int highScore = DataManager.Instance.HighScore;
        highScoreText.GetComponent<TMPro.TextMeshProUGUI>().text = highScore.ToString();
    }

    private void ShowLevelEndedUI(){
        pauseButton.SetActive(false);
        levelEndedUI.SetActive(true);
        yourScoreText.SetActive(true);
    }

    public void SetPanelTransparent(){
        panel.GetComponent<Image>().color = new(0, 0, 0, 0.6f);
    }

    public void SetPanelOpaque(){
        panel.GetComponent<Image>().color = new(0, 0, 0, 0);
    }
}
