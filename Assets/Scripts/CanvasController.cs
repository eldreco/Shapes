using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public Image panel;
    public GameObject levelEndedUI;
    public GameObject scoreText;
    public GameObject highScoreText;
    public GameObject yourScoreText;
    public GameObject gamePausedUI;
    public GameObject pauseButton;

    public GameObject gameManager;

    [SerializeField] SaveData saveData;

    private Animator anim;

    static int highScore;
    public int score;

    private bool gamePaused = false;

    private void Awake() {
        saveData.StartGameFirstTime();
    }

    private void Start() {
        anim = GetComponent<Animator>();
        LevelStarted();
    }

    private void Update() {
        if(!gameManager.GetComponent<GameManager>().GetLevelEnded()){
            SetScore();
            SetHighScore();
        }else{
            LevelEnded();
        }
    }

    private void SetScore(){
        score = gameManager.GetComponent<GameManager>().getPlayer().GetComponent<PlayerController>().getScore();
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString();
    }

    private void SetHighScore(){
        highScoreText.GetComponent<TMPro.TextMeshProUGUI>().text = highScore.ToString();
        if(score >= highScore)
            highScore = score;
    }

    public int GetHighScore(){
        return highScore;
    }

    public void LevelStarted(){
        gamePausedUI.SetActive(false);
        gameObject.SetActive(true);
        anim.SetTrigger("LevelStarted");
        scoreText.SetActive(true);
        levelEndedUI.SetActive(false);
        yourScoreText.SetActive(false);
    }

    public void pauseGame(){
        gamePaused = true;
        gamePausedUI.SetActive(true);
    }

    public void resumeGame(){
        gamePaused = false;
        gamePausedUI.SetActive(false);
    }
    
    public bool gameIsPaused(){
        return gamePaused;
    }

    private void LevelEnded(){
        anim.SetTrigger("LevelEnded");
        pauseButton.SetActive(false);
        levelEndedUI.SetActive(true);
        yourScoreText.SetActive(true);
        saveData.SetHighScore(highScore);
    }
}
