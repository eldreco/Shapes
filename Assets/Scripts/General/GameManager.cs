using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using static MobileControl.MobileControl;
using TimerUtils;
using static Constants.Constants;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool IsLevelEnded {get; set;}
    public bool IsGamePaused {get; set;}
    public int Score {get; set;}

    private float topObstacleVelocity;
    public readonly float Acceleration = 1.02f;
    private float obstacleVelocity = 5f;
    public float ObstacleVelocity
    {
        get => obstacleVelocity;
        set => obstacleVelocity = (value <= topObstacleVelocity) ? value : obstacleVelocity;
    }
    private Timer increaseVelocityTimer;

    public event Action OnLevelEnded;

    private void Awake() {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    private void Start() {
        DataManager.Instance.LoadHighScore();
        SwipeDistance = SceneManager.GetActiveScene().name.Equals(MAIN_MENU_SCENE) 
            ? 25 
            : 5;
    }

    private void OnEnable() {
        increaseVelocityTimer = new(1f);
        increaseVelocityTimer.OnTimerFinished += IncreaseVelocity;
        PlayerController.OnObstaclePassed += UpdateScore;
        SetBaseVelocity();
    }

    private void OnDisable() {
        increaseVelocityTimer.OnTimerFinished -= IncreaseVelocity;
        PlayerController.OnObstaclePassed -= UpdateScore;
    }

    private void Update() {
        increaseVelocityTimer.ExecuteTimer();
    }

    private void UpdateScore(){
        Score++;
        int highScore = DataManager.Instance.HighScore;
        if(Score >= highScore){
            highScore = Score;
            DataManager.Instance.SetHighScore(highScore);
            Debug.Log("Saved high score: " + DataManager.Instance.HighScore);
        }
    }

    private void IncreaseVelocity(){
        ObstacleVelocity *= Acceleration;
        increaseVelocityTimer.ResetTimer();
    }

    public void PauseGame(){
        IsGamePaused = true;
        Time.timeScale = 0;
    }

    public void ResumeGame(){
        IsGamePaused = false;
        Time.timeScale = 1;
    }

    public void ReloadCurrentLevel(){
        if(IsLevelEnded || IsGamePaused){
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void LevelEnded(){
        IsLevelEnded = true;
        ObstacleVelocity = 0;
        OnLevelEnded?.Invoke();
    }

    public void OpenMainMenu(){
        Time.timeScale = 1;
        SceneManager.LoadScene(MAIN_MENU_SCENE, LoadSceneMode.Single);
    }

    public void OpenTutorial(){
        Time.timeScale = 1;
        SceneManager.LoadScene(TUTORIAL_SCENE, LoadSceneMode.Single);
    }

    public void OpenClassic(){
        Time.timeScale = 1;
        SceneManager.LoadScene(CLASSIC_SCENE, LoadSceneMode.Single);
    }

    public void OpenShapes(){
        Time.timeScale = 1;
        SceneManager.LoadScene(SHAPES_SCENE, LoadSceneMode.Single);
    }

    public void SetBaseVelocity(){
        ObstacleVelocity = 5f;
        topObstacleVelocity = 15f;
    }
}
