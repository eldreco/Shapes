using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCanvasController : CanvasManager
{
    public static TutorialCanvasController Instance;

    [SerializeField] private Image panel;
    [SerializeField] private GameObject gamePausedUI;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject playerDiedUI;
    [SerializeField] private GameObject secondStageUI;
    [SerializeField] private GameObject thirdStageUI;
    [SerializeField] private GameObject endUI;
    [SerializeField] private GameObject startStageUI;

    private bool gamePaused = false;

    private Animator anim;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    private void OnEnable() {
        PlayerController.OnPlayerDied += SetPlayerDiedUI;
    }

    private void OnDisable() {
        PlayerController.OnPlayerDied -= SetPlayerDiedUI;
    }

    private void Start() {
        // startStageUI = panel.transform.Find("Start Stage UI").gameObject;
        // playerDiedUI = panel.transform.Find("Player Died UI").gameObject;
        // secondStageUI = panel.transform.Find("Second Stage UI").gameObject;
        // thirdStageUI = panel.transform.Find("Third Stage UI").gameObject;
        endUI = panel.transform.Find("End UI").gameObject;
        LevelStarted();
        anim = gameObject.GetComponent<Animator>();
    }

    private void Update() {
        if(GameManager.Instance.IsLevelEnded)
            LevelEnded();
    }

    public void TutStartStage(){
        ActivateUI(startStageUI);
        DeActivateUI(new GameObject[] {pauseButton, gamePausedUI, endUI});
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

    private void SetPlayerDiedUI(){
        ActivateUI(playerDiedUI);
        anim.SetTrigger("PlayerDied");
    }
}
