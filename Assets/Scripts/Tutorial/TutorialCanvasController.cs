using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCanvasController : CanvasManager
{
    public Image panel;
    public GameObject gamePausedUI;
    public GameObject pauseButton;
    public GameObject playerDiedUI;
    public GameObject secondStageUI;
    public GameObject thirdStageUI;
    public GameObject endUI;
    public GameObject gameManager;

    static bool firstPlay;
    static int highScore;
    public int score;

    private int highScoreSaveData;

    private bool gamePaused = false;

    public GameObject startStageUI;

    private void Awake() {

    }

    private void Start() {
        startStageUI = panel.transform.Find("Start Stage UI").gameObject;
        playerDiedUI = panel.transform.Find("Player Died UI").gameObject;
        secondStageUI = panel.transform.Find("Second Stage UI").gameObject;
        thirdStageUI = panel.transform.Find("Third Stage UI").gameObject;
        endUI = panel.transform.Find("End UI").gameObject;


        LevelStarted();
        


    }

    private void Update() {
        if(!gameManager.GetComponent<GameManager>().GetLevelEnded()){

        }else{
            LevelEnded();
        }
    }

    public void TutStartStage(){
        ActivateUI(startStageUI);
        DeActivateUI(new GameObject[] {pauseButton, gamePausedUI, endUI});
        Debug.Log("Start stage canvas");
    }

    public void TutFirstStage(){
        ActivateUI(pauseButton);
        DeActivateUI(startStageUI);
    }

    public void tutSecondStage(){
        ActivateUI(secondStageUI);
    }

    public void tutThirdStage(){
        ActivateUI(thirdStageUI);
    }

    public void EndUI(){
        ActivateUI(endUI);
    }

    public void playerIsDeadUI(){
        ActivateUI(playerDiedUI);
        DeActivateUI(pauseButton);
    }

    public void LevelStarted(){
        DeActivateUI(gamePausedUI);
        DeActivateUI(playerDiedUI);
    }

    public void pauseGame(){
        gamePaused = true;
        ActivateUI(gamePausedUI);
        DeActivateUI(pauseButton);
    }

    public void resumeGame(){
        gamePaused = false;
        DeActivateUI(gamePausedUI);
        ActivateUI(pauseButton);
    }
    
    public bool gameIsPaused(){
        return gamePaused;
    }

    private void LevelEnded(){
        pauseButton.SetActive(false);
    }
}
