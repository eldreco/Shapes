using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject _player;
    public PlayerController _playerController{get; private set;}

    public bool _levelEnded {get; private set;}
    public float _obstacleVelocity {get; private set;}
    private float _topVelocity;
    private float _acceleration = 0.5f;
    private float _timer = 0;
    private float _increaseInterval = 1.0f;

    private void Awake() {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        _playerController = _player.GetComponent<PlayerController>();
    }

    private void Start() {
        SetBaseVelocity();
    }

    private void Update() {
        UpdateGame();
    }
    
    protected void UpdateGame(){
        _levelEnded = _playerController._levelEnded;        
        if(Time.time >= _timer && _obstacleVelocity <= _topVelocity){
            _timer = (int)(Time.time + _increaseInterval);
            _obstacleVelocity *= 1.01f ;
        }
        if(_obstacleVelocity > _topVelocity)
            _obstacleVelocity = _topVelocity;
        if(_levelEnded)
            _obstacleVelocity = 0;
    }

    public void PauseGame(){
        CanvasController.Instance.pauseGame();
        _playerController.setPause(true);
        Time.timeScale = 0;
    }

    public void ResumeGame(){
        CanvasController.Instance.resumeGame();
        _playerController.setPause(false);
        Time.timeScale = 1;
    }

    public void ReloadCurrentLevel(){
        if(_playerController._levelEnded || CanvasController.Instance._gamePaused){
            Time.timeScale = 1; //Because of restarting when paused
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            CanvasController.Instance.LevelStarted();
        }
    }

    public void OpenMainMenu(){
        Time.timeScale = 1; //Because it's clicked when menus are opened
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }

    public void OpenTutorial(){
        Time.timeScale = 1; //Because it's clicked when menus are opened
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
    }

    public void OpenClassic(){
        Time.timeScale = 1; //Because it's clicked when menus are opened
        SceneManager.LoadScene("Classic", LoadSceneMode.Single);
    }

    public void OpenShapes(){
        Time.timeScale = 1; //Because it's clicked when menus are opened
        SceneManager.LoadScene("Shapes", LoadSceneMode.Single);
    }

    public void SetBaseVelocity(){
        _obstacleVelocity = 5f; //CHANGE IF NEEDED
        _topVelocity = 15f;
    }

    public void SetVelocity(float v){
        _obstacleVelocity = v;
    }

    public void SetTopVelocity(float v){
        _topVelocity = v;
    }
}
