using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public static CanvasController Instance;

    [SerializeField] private Image _panel;
    [SerializeField] private GameObject _levelEndedUI;
    [SerializeField] private GameObject _scoreText;
    [SerializeField] private GameObject _highScoreText;
    [SerializeField] private GameObject _yourScoreText;
    [SerializeField] private GameObject _gamePausedUI;
    [SerializeField] private GameObject _pauseButton;

    private Animator _anim;

    private int _highScore;
    private int _score;

    public bool _gamePaused {get; private set;} = false;

    private PlayerController _playerController;

    private void Awake() {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    private void Start() {
        _highScore = DataManager.Instance._highScore;
        _anim = GetComponent<Animator>();
        _playerController = GameManager.Instance._playerController;
        Debug.Log("Player controller: " + _playerController);
        LevelStarted();
    }

    private void Update() {
        if(!GameManager.Instance._levelEnded){
            SetScore();
            SetHighScore();
        }else
            LevelEnded();
    }

    private void SetScore(){
        _score = _playerController.getScore();
        _scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = _score.ToString();
    }

    private void SetHighScore(){
        _highScoreText.GetComponent<TMPro.TextMeshProUGUI>().text = _highScore.ToString();
        if(_score >= _highScore){
            _highScore = _score;
            DataManager.Instance.SetHighScore(_highScore);
        }
    }

    public void LevelStarted(){
        _gamePausedUI.SetActive(false);
        gameObject.SetActive(true);
        _anim.SetTrigger("LevelStarted");
        _scoreText.SetActive(true);
        _levelEndedUI.SetActive(false);
        _yourScoreText.SetActive(false);
    }

    public void pauseGame(){
        _gamePaused = true;
        _gamePausedUI.SetActive(true);
    }

    public void resumeGame(){
        _gamePaused = false;
        _gamePausedUI.SetActive(false);
    }

    private void LevelEnded(){
        _anim.SetTrigger("LevelEnded");
        _pauseButton.SetActive(false);
        _levelEndedUI.SetActive(true);
        _yourScoreText.SetActive(true);
    }
}
