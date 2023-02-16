using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTutorialController : PlayerController
{

    private bool move;
    private TutorialManager tutorialManager;
    private void Start() {
        move = true;
        _anim = GetComponent<Animator>();
        tutorialManager = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();
    }

    private void Update() {
        SwipeCheck();
        // SwipeControl();
        PCControl();
        PosControl();
        IsDownTimer();
        IsUpTimer();

    }

    //New SwipeControl to ignore parent one,
    //checks the tutorialStage
    public new void SwipeControl(){
        Vector2 Length = _swipeEndPos - _swipeStartPos;
        float xLength = Mathf.Abs(Length.x);
        float yLength = Mathf.Abs(Length.y);

        if(!_gamePaused){
            if(xLength > yLength){
                if(Length.x > 0 && !_isUp && move){
                    goRight();
                    Debug.Log("Go right");
                } else if( Length.x < 0 && !_isUp && move){
                    goLeft();
                    Debug.Log("Go left");
                }
            } else if(xLength < yLength){
                if(Length.y > 0 && (IsActiveStage(TutorialManager.TutorialStage.UpObs) || _isDown)){
                    goUP();
                } else if(Length.y < 0 && !_isDown && IsActiveStage(TutorialManager.TutorialStage.DownObs)){
                    goDown();
                }
            }
        }
    }

    private new void PCControl(){
        if(!_gamePaused){
            if(!_isUp && move){
                if(Input.GetKeyDown(KeyCode.LeftArrow))
                    goLeft();
                else if(Input.GetKeyDown(KeyCode.RightArrow))
                    goRight();
            }
            if(Input.GetKeyDown(KeyCode.UpArrow)  && (IsActiveStage(TutorialManager.TutorialStage.UpObs) || _isDown))
                goUP();
            else if(Input.GetKeyDown(KeyCode.DownArrow) && !_isDown && IsActiveStage(TutorialManager.TutorialStage.DownObs))
                goDown();
        }
    }

    private void OnCollisionEnter(Collision other){
        Vector3 diePos = new Vector3(transform.position.x , 0.5f , transform.position.z);
        if(other.gameObject.tag == "Obstacle"){
            Instantiate(_dieEffect , diePos , transform.rotation);
            // gameObject.SetActive(false);
            // levelEnded = true;
            
            // tutorialManager.ReloadStage();
            // tutorialManager.PlayerDead();
            tutorialManager.setTimer(true);
            tutorialManager.PlayerDead();
        }
    }

    private bool IsActiveStage(TutorialManager.TutorialStage stage){
        return tutorialManager.GetTutorialStage() == stage;
    }

    public void SetMove(bool val){
        move = val;
    }
}
