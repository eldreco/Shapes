using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject player;
    public GameObject floor;
    public GameObject gameManager;
    public GameObject tutorialObs;

    public GameObject mainMenu;
    private MainMenuCanvasController mMenuCanvasManager;

    private bool tutorialOpen;
    private bool loadedTutorial;

    // Start is called before the first frame update
    void Start()
    {
        mMenuCanvasManager = mainMenu.GetComponent<MainMenuCanvasController>();
        tutorialOpen = false;
        loadedTutorial = false;
    }

    // Update is called once per frame
    void Update()
    {
        // controlPlayerBeforeTutorial();
    }

    // private void setTutorialUI(){
    //     Time.timeScale = 0;
    //     mMenuCanvasManager.openTutorialUI();
    //     tutorialOpen = true;
    // }

    // private void unsetTutorialUI(){
    //     Time.timeScale = 1;
    //     mMenuCanvasManager.closeTutorialUI();
    //     tutorialOpen = false;
    // }

    // public void playerEnterTrigger(Collider other){
    //     if(other.tag == "TutorialFront"){
    //         Debug.Log("Set tutorialUI");
    //         setTutorialUI();
    //     }
    // }

    // public void playerExitTrigger(Collider other){
    //     if(other.tag == "TutorialBack" && !loadedTutorial){
    //         loadedTutorial = true;
    //         SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
    //     }
    // }

    // private void controlPlayerBeforeTutorial(){
    //     if(tutorialOpen && SceneManager.GetActiveScene().name == "Main Menu"){
    //         if(player.GetComponent<PlayerController>().getPos() == 0 || player.GetComponent<PlayerController>().getPos() == 2){
    //             unsetTutorialUI();
    //             Debug.Log("Unset Tutorial");
    //         }
    //     }
    // }
}
