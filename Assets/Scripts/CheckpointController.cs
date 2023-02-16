using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : ObstacleController
{
    public TutorialManager tutorialManager;
    public TutorialCanvasController canvasController;

    private new void Start() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>().gameObject;
        tutorialManager = gameManager.GetComponent<TutorialManager>();
        canvasController = FindObjectOfType<Canvas>().GetComponent<TutorialCanvasController>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            switch (tutorialManager.GetTutorialStage())
            {
                case TutorialManager.TutorialStage.MainObs:
                    tutorialManager.getPlayer().GetComponent<PlayerTutorialController>().SetMove(false);
                    canvasController.tutSecondStage();
                    tutorialManager.SetDownObs();
                    break;
                case TutorialManager.TutorialStage.DownObs:
                    tutorialManager.getPlayer().GetComponent<PlayerTutorialController>().SetMove(false);
                    canvasController.tutThirdStage();
                    tutorialManager.SetUpObs();
                    break;
                case TutorialManager.TutorialStage.UpObs:
                    tutorialManager.SetFinal();
                    canvasController.EndUI();
                    break;

            }
            Debug.Log("Checkpoint");        
        }

    }
}
