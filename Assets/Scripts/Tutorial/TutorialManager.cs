using System;
using UnityEngine;
using static PlayerUtils.PlayerUtils;
using TimerUtils;
using TutorialUtils;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    public bool CheckTimer {get; set;}

    private readonly Timer playerRespawnTimer = new(5f);

    [SerializeField] private GameObject[] stages;

    private int currentStageIndex;

    private void Awake(){
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    private void OnEnable() {
        PlayerController.OnPlayerDied += ManagePlayerDied;
        playerRespawnTimer.OnTimerFinished += ReloadStage;
        PlayerController.OnPlayerMovedH += CheckValidHMovement;
        PlayerController.OnPlayerMovedV += CheckValidVMovement;
    }

    private void OnDisable() {
        PlayerController.OnPlayerDied -= ManagePlayerDied;
        playerRespawnTimer.OnTimerFinished -= ReloadStage;
        PlayerController.OnPlayerMovedH -= CheckValidHMovement;
        PlayerController.OnPlayerMovedV -= CheckValidVMovement;
    }

    private void Start(){
        SpawnObstacles.Instance.SetSpawnType(false, true, false);
        foreach (var currentDelegate in PlayerController.OnObstaclePassed.GetInvocationList())
            PlayerController.OnObstaclePassed = (Action)Delegate.Remove(PlayerController.OnObstaclePassed, currentDelegate);

        currentStageIndex = 0;
        stages[currentStageIndex].GetComponent<IStage>().ShowStageInstructions();
    }

    private void Update(){
        if(CheckTimer) playerRespawnTimer.ExecuteTimer();
    }

    private void OnTriggerEnter(Collider other) {
        if(!other.CompareTag("Checkpoint")) return;
        currentStageIndex++;
        if(currentStageIndex < stages.Length) 
            stages[currentStageIndex].GetComponent<IStage>().ShowStageInstructions();
        else 
            TutorialEnded();
    }

    private void CheckValidHMovement(HorizontalPos pos){
        if(stages[currentStageIndex].GetComponent<IStage>().GetNeededMovement().Equals(pos.ToString())){
            stages[currentStageIndex].GetComponent<IStage>().BeginStage();
        }
    }

    private void CheckValidVMovement(VerticalPos pos){
        if(stages[currentStageIndex].GetComponent<IStage>().GetNeededMovement().Equals(pos.ToString())){
            stages[currentStageIndex].GetComponent<IStage>().BeginStage();
        }
    }

    private void TutorialEnded(){
        Debug.Log("Tutorial ended");
    }

    public void PauseGame(){
        GameManager.Instance.IsGamePaused = true;
        Time.timeScale = 0;
        TutorialCanvasController.Instance.pauseGame();
    }

    public void ResumeGame(){
        GameManager.Instance.IsGamePaused = false;
        TutorialCanvasController.Instance.resumeGame();
        Time.timeScale = 1;
    }
    
    public void ManagePlayerDied(){
        PlayerController.Instance.gameObject.SetActive(false);
        SpawnObstacles.Instance.ResetCount();
        SpawnTutorial.Instance.ResetCheckpoint();
        GameObject[] obs = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach(GameObject o in obs) Destroy(o);
        Destroy(GameObject.FindGameObjectWithTag("Checkpoint"));
        CheckTimer = true;
    }

    private void ReloadStage(){
        CheckTimer = false;
        PlayerController.Instance.gameObject.SetActive(true);
        PlayerController.Instance.HPos = HorizontalPos.Middle;
        GameManager.Instance.SetBaseVelocity();
        GameManager.Instance.IsLevelEnded = false;
        TutorialCanvasController.Instance.LevelStarted();
        FloorController.Instance.Reset();
    }

}
