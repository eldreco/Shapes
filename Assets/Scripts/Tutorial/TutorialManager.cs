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

    public static new TutorialManager Instance;

    [SerializeField] private GameObject _spawn;

    public TutorialStage _activeStage{get;private set;}

    public bool _checkTimer {get; set;} = false;
    private float _timerDeadVal = 5f;
    private float _timerDead = 0;

    private TutorialCanvasController _tutorialCanvasController;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    void Start()
    {
        _timerDead = _timerDeadVal;
        SetStart();
    }

    void Update()
    {
        UpdateStage();
    }

    public void UpdateStage(){
        PlayerDeadTimer();
        if(_activeStage == TutorialStage.Start){
            if(PlayerController.Instance.getPos() == 0 || PlayerController.Instance.getPos() == 2)
                SetMainObs();
        }
        else if(_activeStage == TutorialStage.DownObs){
            if(PlayerController.Instance.getIsDown()){
                Time.timeScale = 1;
                PlayerTutorialController.Instance.SetMove(true);
                TutorialCanvasController.Instance.DeActivateUI(TutorialCanvasController.Instance._secondStageUI);
            }
        }
        else if(_activeStage == TutorialStage.UpObs){
            if(PlayerController.Instance.getIsUp()){
                PlayerTutorialController.Instance.SetMove(true);
                Time.timeScale = 1;
                TutorialCanvasController.Instance.DeActivateUI(TutorialCanvasController.Instance._thirdStageUI);
            }
        }
    }

    public new void PauseGame(){
        PlayerTutorialController.Instance.setPause(true);
        Time.timeScale = 0;
        TutorialCanvasController.Instance.pauseGame();
        Debug.Log("Game Paused");
    }

    public new void ResumeGame(){
        PlayerTutorialController.Instance.setPause(false);
        TutorialCanvasController.Instance.resumeGame();
        Time.timeScale = 1;
    }

    private void SetStart(){
        _activeStage = TutorialStage.Start;
        TutorialCanvasController.Instance.TutStartStage();
        Time.timeScale = 0;
    }

    private void SetMainObs(){
        _activeStage = TutorialStage.MainObs;
        TutorialCanvasController.Instance.TutFirstStage();
        Time.timeScale = 1;
        SpawnTutorial.Instance.setSpawnType(false, true, false);
        SetBaseVelocity();
        SpawnTutorial.Instance._isActive = true;
    }

    public void SetDownObs(){
        _activeStage = TutorialStage.DownObs;
        SpawnTutorial.Instance.setSpawnType(true, false, false);
        SpawnTutorial.Instance.ResetCount();
        SetBaseVelocity();
        SpawnTutorial.Instance._isActive = true;
    }

    public void SetUpObs(){
        _activeStage = TutorialStage.UpObs;
        SpawnTutorial.Instance.setSpawnType(false, false, true);
        SpawnTutorial.Instance.ResetCount();
        SetBaseVelocity();
        SpawnTutorial.Instance._isActive = true;
    }

    public void SetFinal(){
        Time.timeScale = 0;
        _activeStage = TutorialStage.End;
        Debug.Log("Final Stage");
    }
    
    public void PlayerDead(){
        _player.SetActive(false);
        TutorialCanvasController.Instance.ActivateUI(_tutorialCanvasController._playerDiedUI);
        TutorialCanvasController.Instance.GetComponent<Animator>().SetTrigger("PlayerDied");
        SpawnTutorial.Instance.ResetCount();
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
                _checkTimer = false;
                _timerDead = _timerDeadVal;
            }
        }
    }

    private void ReloadStage(){
        _player.SetActive(true);
        PlayerController.Instance.setPos(1);
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
