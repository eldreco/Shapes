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
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            switch (TutorialManager.Instance._activeStage)
            {
                case TutorialManager.TutorialStage.MainObs:
                    PlayerTutorialController.Instance.SetMove(false);
                    TutorialCanvasController.Instance.tutSecondStage();
                    TutorialManager.Instance.SetDownObs();
                    break;
                case TutorialManager.TutorialStage.DownObs:
                    PlayerTutorialController.Instance.SetMove(false);
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
