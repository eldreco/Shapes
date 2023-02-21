using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : GameManager
{
    public enum TutorialStage
    {
        Start,
        MainObs,
        DownObs,
        UpObs,
        End
    }

    public static TutorialManager Instance;

    [SerializeField] private GameObject _spawn;
    private SpawnObstacles _spawnObstacles;

    public TutorialStage _activeStage{get;private set;}

    public bool _checkTimer {get; set;} = false;
    private float _timerDeadVal = 5f;
    private float _timerDead = 0;

    private TutorialCanvasController _tutorialCanvasController;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        
        Instance = this;
    }

    private void SetObjects(){
        canvas = GameObject.Find("Tutorial Canvas");
        _spawnObstacles = _spawn.GetComponent<SpawnTutorial>();
        _tutorialCanvasController = canvas.GetComponent<TutorialCanvasController>();
    }

    void Start()
    {
        _timerDead = _timerDeadVal;
        SetObjects();
        SetStart();
    }

    void Update()
    {
        UpdateStage();
    }

    public void UpdateStage(){
        PlayerDeadTimer();
        if(_activeStage == TutorialStage.Start){
            if(player.GetComponent<PlayerController>().getPos() == 0 || player.GetComponent<PlayerController>().getPos() == 2)
                SetMainObs();
        }
        else if(_activeStage == TutorialStage.DownObs){
            if(player.GetComponent<PlayerController>().getIsDown()){
                Time.timeScale = 1;
                player.GetComponent<PlayerTutorialController>().SetMove(true);
                _tutorialCanvasController.DeActivateUI(_tutorialCanvasController._secondStageUI);
            }
        }
        else if(_activeStage == TutorialStage.UpObs){
            if(player.GetComponent<PlayerController>().getIsUp()){
                player.GetComponent<PlayerTutorialController>().SetMove(true);
                Time.timeScale = 1;
                _tutorialCanvasController.DeActivateUI(_tutorialCanvasController._thirdStageUI);
            }
        }
    }

    public new void pauseGame(){
        player.GetComponent<PlayerTutorialController>().setPause(true);
        Time.timeScale = 0;
        canvas.GetComponent<TutorialCanvasController>().pauseGame();
        Debug.Log("Game Paused");
    }

    public new void resumeGame(){
        player.GetComponent<PlayerTutorialController>().setPause(false);
        canvas.GetComponent<TutorialCanvasController>().resumeGame();
        Time.timeScale = 1;
    }

    private void SetStart(){
        _activeStage = TutorialStage.Start;
        Debug.Log("Start Stage");
        canvas.GetComponent<TutorialCanvasController>().TutStartStage();
        Time.timeScale = 0;
    }

    private void SetMainObs(){
        _activeStage = TutorialStage.MainObs;
        Debug.Log("First Stage");
        canvas.GetComponent<TutorialCanvasController>().TutFirstStage();
        Time.timeScale = 1;
        _spawnObstacles.setSpawnType(false, true, false);
        SetBaseVelocity();
        SpawnTutorial.Instance._isActive = true;
    }

    public void SetDownObs(){
        _activeStage = TutorialStage.DownObs;
        Debug.Log("Second Stage");
        _spawnObstacles.setSpawnType(true, false, false);
        _spawnObstacles.ResetCount();
        SetBaseVelocity();
        SpawnTutorial.Instance._isActive = true;
    }

    public void SetUpObs(){
        _activeStage = TutorialStage.UpObs;
        Debug.Log("Third Stage");
        _spawnObstacles.setSpawnType(false, false, true);
        _spawnObstacles.ResetCount();
        SetBaseVelocity();
        SpawnTutorial.Instance._isActive = true;
    }

    public void SetFinal(){
        Time.timeScale = 0;
        _activeStage = TutorialStage.End;
        Debug.Log("Final Stage");
    }
    
    public void PlayerDead(){
        player.SetActive(false);
        _tutorialCanvasController.ActivateUI(_tutorialCanvasController._playerDiedUI);
        _tutorialCanvasController.GetComponent<Animator>().SetTrigger("PlayerDied");
        _spawnObstacles.ResetCount();
        _spawn.GetComponent<SpawnTutorial>().ResetCheckpoint();
        GameObject[] obs = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach(GameObject o in obs) Destroy(o);
        Destroy(GameObject.FindGameObjectWithTag("Checkpoint"));
        _checkTimer = true;
    }

    private void PlayerDeadTimer(){
        if(_checkTimer){
            _timerDead -= Time.deltaTime;
            if(_timerDead <= 0){
                ReloadStage();
                Debug.Log("Timer reached 0");
                _checkTimer = false;
                _timerDead = _timerDeadVal;
            }
        }
    }

    private void ReloadStage(){
        player.SetActive(true);
        player.GetComponent<PlayerController>().setPos(1);
        SetTutorialStage(this._activeStage);
        SetBaseVelocity();
        _tutorialCanvasController.DeActivateUI(_tutorialCanvasController._playerDiedUI);
    }

    public void SetTutorialStage(TutorialStage stage){
        switch (stage)
        {  
            case TutorialStage.MainObs:
                SetMainObs();
                break;
            case TutorialStage.DownObs:
                SetDownObs();
                break;
            case TutorialStage.UpObs:
                SetUpObs();
                break;
        }
    }
}
