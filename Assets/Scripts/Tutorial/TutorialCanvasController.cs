using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCanvasController : CanvasManager
{
    public static TutorialCanvasController Instance;

    [SerializeField] private Image _panel;
    [SerializeField] private GameObject _gamePausedUI;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] public GameObject _playerDiedUI {get; private set;}
    [SerializeField] public GameObject _secondStageUI {get; private set;}
    [SerializeField] public GameObject _thirdStageUI {get; private set;}
    [SerializeField] private GameObject _endUI;
    [SerializeField] private GameObject _startStageUI;
    [SerializeField] private GameObject _gameManager;

    private bool gamePaused = false;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    private void Start() {
        _startStageUI = _panel.transform.Find("Start Stage UI").gameObject;
        _playerDiedUI = _panel.transform.Find("Player Died UI").gameObject;
        _secondStageUI = _panel.transform.Find("Second Stage UI").gameObject;
        _thirdStageUI = _panel.transform.Find("Third Stage UI").gameObject;
        _endUI = _panel.transform.Find("End UI").gameObject;
        LevelStarted();
    }

    private void Update() {
        if(!_gameManager.GetComponent<GameManager>().GetLevelEnded()){

        }else{
            LevelEnded();
        }
    }

    public void TutStartStage(){
        ActivateUI(_startStageUI);
        DeActivateUI(new GameObject[] {_pauseButton, _gamePausedUI, _endUI});
    }

    public void TutFirstStage(){
        ActivateUI(_pauseButton);
        DeActivateUI(_startStageUI);
    }

    public void tutSecondStage(){
        ActivateUI(_secondStageUI);
    }

    public void tutThirdStage(){
        ActivateUI(_thirdStageUI);
    }

    public void EndUI(){
        ActivateUI(_endUI);
    }

    public void playerIsDeadUI(){
        ActivateUI(_playerDiedUI);
        DeActivateUI(_pauseButton);
    }

    public void LevelStarted(){
        DeActivateUI(_gamePausedUI);
        DeActivateUI(_playerDiedUI);
    }

    public void pauseGame(){
        gamePaused = true;
        ActivateUI(_gamePausedUI);
        DeActivateUI(_pauseButton);
    }

    public void resumeGame(){
        gamePaused = false;
        DeActivateUI(_gamePausedUI);
        ActivateUI(_pauseButton);
    }
    
    public bool gameIsPaused(){
        return gamePaused;
    }

    private void LevelEnded(){
        _pauseButton.SetActive(false);
    }
}
