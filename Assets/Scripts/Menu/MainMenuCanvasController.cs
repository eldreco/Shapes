using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuCanvasController : CanvasManager
{   

    public static MainMenuCanvasController Instance;

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Image _panel;
    [SerializeField] private GameObject _menuButton;
    [SerializeField] private GameObject _settingsButton;
    [SerializeField] private GameObject _menuOpenUI;
    [SerializeField] private GameObject _settingsOpenUI;
    [SerializeField] private GameObject _tutorialUI;
    [SerializeField] private GameObject _mainMenuText;

    //So its only either opened or closed
    private bool _menuOpened;
    private bool _settingsOpened;
    private bool _firstEnterMenu;

    private void Awake() {
         if (Instance != null)
            Destroy(gameObject);
        
        Instance = this;
    }

    void Start()
    {
        //Start with menus closed
        closeAllUI();
        ActivateUI(new GameObject[] {_menuButton, _settingsButton});
    }

    public void clickMenuButton(){
        if(!_menuOpened) openMenu(); else closeMenu();
    }

    private void closeAllUI(){
        closeMenu();
        closeSettings();
        closeTutorialUI();
    }

    private void openMenu(){
        closeSettings(); //So both menus aren't open at the same time
        _menuOpenUI.SetActive(true);
        ActivateUI(_menuOpenUI);
        DeActivateUI(_mainMenuText);
        Time.timeScale = 0; 
        _menuOpened = true;
        _gameManager.GetPlayer().GetComponent<PlayerController>().SetCanMove(false);
    }

    private void closeMenu(){
        DeActivateUI(_menuOpenUI);
        ActivateUI(_mainMenuText);
        Time.timeScale = 1; 
        _menuOpened = false;
        _gameManager.GetPlayer().GetComponent<PlayerController>().SetCanMove(true);
    }

    public void clickSettingsButton(){
        if(!_settingsOpened){
            openSettings();
            DeActivateUI(_mainMenuText);
            _gameManager.GetPlayer().GetComponent<PlayerController>().SetCanMove(false);
        }else {
            closeSettings();
            ActivateUI(_mainMenuText);
            _gameManager.GetPlayer().GetComponent<PlayerController>().SetCanMove(true);
        }
    }

    private void openSettings(){
        closeMenu(); //So both menus aren't open at the same time
        _settingsOpenUI.SetActive(true);
        Time.timeScale = 0; 
        _settingsOpened = true;
    }

    private void closeSettings(){
        _settingsOpenUI.SetActive(false);
        Time.timeScale = 1; 
        _settingsOpened = false;
    }

    private void firstEnteredMenu(){
        if(_firstEnterMenu){
            _menuButton.SetActive(false);
            _settingsButton.SetActive(false);
        }else{
            _menuButton.SetActive(true);
            _settingsButton.SetActive(true);
        }
    }

    public void openTutorialUI(){
        _tutorialUI.SetActive(true);
    }

    public void closeTutorialUI(){
        _tutorialUI.SetActive(false);
    }

    public bool getFirstEnteredMenu(){
        return _firstEnterMenu;
    }
}
