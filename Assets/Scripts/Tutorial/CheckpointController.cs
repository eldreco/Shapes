using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : ObstacleController
{
    public static CheckpointController Instance;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    private new void Start() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>().gameObject;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            switch (TutorialManager.Instance._activeStage)
            {
                case TutorialManager.TutorialStage.MainObs:
                    TutorialManager.Instance.GetPlayer().GetComponent<PlayerTutorialController>().SetMove(false);
                    TutorialCanvasController.Instance.tutSecondStage();
                    TutorialManager.Instance.SetDownObs();
                    break;
                case TutorialManager.TutorialStage.DownObs:
                    TutorialManager.Instance.GetPlayer().GetComponent<PlayerTutorialController>().SetMove(false);
                    TutorialCanvasController.Instance.tutThirdStage();
                    TutorialManager.Instance.SetUpObs();
                    break;
                case TutorialManager.TutorialStage.UpObs:
                    TutorialManager.Instance.SetFinal();
                    TutorialCanvasController.Instance.EndUI();
                    break;
            }
        }
    }
}
