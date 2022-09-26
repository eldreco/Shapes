using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : GameManager
{
    //Check in which stage the of the tutorial the 
    //player is
    public enum TutorialStage
    {
        Start,
        First,
        Second,
        Third,
        Final
    }

    public GameObject Spawn;
    private SpawnObstacles spawnObstacles;

    public TutorialStage activeStage;

    private bool checkTimer = false;
    private float timerDeadVal = 2f;
    private float timerDead = 0;

    private TutorialCanvasController tutorialCanvasController;

    void Start()
    {
        timerDead = timerDeadVal;
        SetObjects();
        SetStart();
    }

    void Update()
    {
        UpdateStage();
        levelEnded = player.GetComponent<PlayerController>().GetLevelEnded();        
        if(levelEnded)
            obstacleVelocity = 0;
    }

    private void SetObjects(){
        canvas = GameObject.Find("Tutorial Canvas");
        spawnObstacles = Spawn.GetComponent<SpawnTutorial>();
        tutorialCanvasController = canvas.GetComponent<TutorialCanvasController>();
    }

    public void UpdateStage(){
        // activeStage = stage;
        PlayerDeadTimer();
        if(activeStage == TutorialStage.Start){
            if(player.GetComponent<PlayerController>().getPos() == 0 || player.GetComponent<PlayerController>().getPos() == 2)
                SetFirst();
        }
        else if(activeStage == TutorialStage.Second){
            if(player.GetComponent<PlayerController>().getIsDown()){
                Time.timeScale = 1;
                player.GetComponent<PlayerTutorialController>().SetMove(true);
                tutorialCanvasController.DeActivateUI(tutorialCanvasController.secondStageUI);
            }
        }
        else if(activeStage == TutorialStage.Third){
            if(player.GetComponent<PlayerController>().getIsUp()){
                player.GetComponent<PlayerTutorialController>().SetMove(true);
                Time.timeScale = 1;
                tutorialCanvasController.DeActivateUI(tutorialCanvasController.thirdStageUI);
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
        activeStage = TutorialStage.Start;
        Debug.Log("Start Stage");
        canvas.GetComponent<TutorialCanvasController>().TutStartStage();
        Time.timeScale = 0;
    }

    private void SetFirst(){
        activeStage = TutorialStage.First;
        Debug.Log("First Stage");
        canvas.GetComponent<TutorialCanvasController>().TutFirstStage();
        Time.timeScale = 1;
        spawnObstacles.setSpawnType(false, true, false);
        SetBaseVelocity();
        spawnObstacles.GetComponent<SpawnTutorial>().setActive(true);
        StartCoroutine(spawnObstacles.GetComponent<SpawnTutorial>().SpawnStage());

    }

    public void SetSecond(){
        Time.timeScale = 0;
        activeStage = TutorialStage.Second;
        Debug.Log("Second Stage");
        spawnObstacles.setSpawnType(true, false, false);
        SetBaseVelocity();
        spawnObstacles.GetComponent<SpawnTutorial>().setActive(true);
        StartCoroutine(spawnObstacles.GetComponent<SpawnTutorial>().SpawnStage());
    }

    public void SetThird(){
        Time.timeScale = 0;
        activeStage = TutorialStage.Third;
        Debug.Log("Third Stage");
        spawnObstacles.setSpawnType(false, false, true);
        SetBaseVelocity();
        spawnObstacles.GetComponent<SpawnTutorial>().setActive(true);
        StartCoroutine(spawnObstacles.GetComponent<SpawnTutorial>().SpawnStage());
    }

    public void SetFinal(){
        Time.timeScale = 0;
        activeStage = TutorialStage.Final;
        Debug.Log("Final Stage");
    }
    
    public void PlayerDead(){
        // tutorialCanvasController.playerIsDeadUI();
        // Time.timeScale = 0;
        ReloadStage();
    }

    private void PlayerDeadTimer(){
        if(checkTimer){
            SetVelocity(0);
            player.SetActive(false);
            timerDead -= Time.deltaTime;
            if(timerDead <= 0){
                PlayerDead();
                Debug.Log("Timer reached 0");
                checkTimer = false;
                timerDead = timerDeadVal;
            }
        }
        
    }

    public void setTimer(bool value){
        checkTimer = value;
    }

    public void ReloadStage(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        
    }

    public TutorialStage GetTutorialStage(){
        return activeStage;
    }

}
