using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuCanvasController : CanvasManager
{   
    public GameManager manager;
    public Image panel;
    public GameObject menuButton;
    public GameObject settingsButton;
    public GameObject menuOpenUI;
    public GameObject settingsOpenUI;

    public GameObject tutorialUI;
    public GameObject mainMenuText;

    //So its only either opened or closed
    public bool menuOpened;
    public bool settingsOpened;

    public bool firstEnterMenu;

    private void Awake() {
        // if(Application.isEditor == false){
        //     if(PlayerPrefs.GetInt("FirstEnterMenu", 1)==1){
        //         firstEnterMenu = true;
        //         PlayerPrefs.SetInt ("FirstEnterMenu", 0);
        //         PlayerPrefs.Save();
        //     }else{
        //         firstEnterMenu = false;
        //     }
                
        // }
    }

    void Start()
    {
        //Start with menus closed
        closeAllUI();
        ActivateUI(new GameObject[] {menuButton, settingsButton});
        // firstEnteredMenu();
    }

    void Update()
    {
       
    }

    public void clickMenuButton(){
        if(!menuOpened)
            openMenu();
        else 
            closeMenu();
    }

    private void closeAllUI(){
        closeMenu();
        closeSettings();
        closeTutorialUI();
    }

    private void openMenu(){
        closeSettings(); //So both menus aren't open at the same time
        menuOpenUI.SetActive(true);
        ActivateUI(menuOpenUI);
        DeActivateUI(mainMenuText);
        Time.timeScale = 0; //Pause time
        menuOpened = true;
        manager.getPlayer().GetComponent<PlayerController>().SetCanMove(false);
    }

    private void closeMenu(){
        DeActivateUI(menuOpenUI);
        ActivateUI(mainMenuText);
        Time.timeScale = 1; //Resume time
        menuOpened = false;
        manager.getPlayer().GetComponent<PlayerController>().SetCanMove(true);
    }

    public void clickSettingsButton(){
        if(!settingsOpened){
            openSettings();
            DeActivateUI(mainMenuText);
            manager.getPlayer().GetComponent<PlayerController>().SetCanMove(false);
        }else {
            closeSettings();
            ActivateUI(mainMenuText);
            manager.getPlayer().GetComponent<PlayerController>().SetCanMove(true);
        }
    }

    private void openSettings(){
        closeMenu(); //So both menus aren't open at the same time
        settingsOpenUI.SetActive(true);
        Time.timeScale = 0; //Pause time
        settingsOpened = true;
    }

    private void closeSettings(){
        settingsOpenUI.SetActive(false);
        Time.timeScale = 1; //Resume time
        settingsOpened = false;
    }



    public void startClassic(){
        Time.timeScale = 1; //Because it's clicked when menus are opened
        SceneManager.LoadScene("Classic", LoadSceneMode.Single);
    }

    private void firstEnteredMenu(){
        if(firstEnterMenu){
            menuButton.SetActive(false);
            settingsButton.SetActive(false);
        }else{
            menuButton.SetActive(true);
            settingsButton.SetActive(true);
        }
    }

    public void openTutorialUI(){
        tutorialUI.SetActive(true);
    }

    public void closeTutorialUI(){
        tutorialUI.SetActive(false);
    }

    public bool getFirstEnteredMenu(){
        return firstEnterMenu;
    }
}
