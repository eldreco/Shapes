using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTutorialController : PlayerController
{

    private bool move;
    private TutorialManager tutorialManager;
    private void Start() {
        move = true;
        anim = GetComponent<Animator>();
        tutorialManager = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();
    }

    private void Update() {
        SwipeCheck();
        // SwipeControl();
        PCControl();
        posControl();
        IsDownTimer();
        IsUpTimer();

    }

    //New SwipeControl to ignore parent one,
    //checks the tutorialStage
    public new void SwipeControl(){
        Vector2 Length = swipeEndPos - swipeStartPos;
        float xLength = Mathf.Abs(Length.x);
        float yLength = Mathf.Abs(Length.y);

        if(!gamePaused){
            if(xLength > yLength){
                if(Length.x > 0 && !isUp && move){
                    goRight();
                    Debug.Log("Go right");
                } else if( Length.x < 0 && !isUp && move){
                    goLeft();
                    Debug.Log("Go left");
                }
            } else if(xLength < yLength){
                if(Length.y > 0 && (IsActiveStage(TutorialManager.TutorialStage.Third) || isDown)){
                    goUP();
                } else if(Length.y < 0 && !isDown && IsActiveStage(TutorialManager.TutorialStage.Second)){
                    goDown();
                }
            }
        }
    }

    private new void PCControl(){
        if(!gamePaused){
            if(!isUp && move){
                if(Input.GetKeyDown(KeyCode.LeftArrow))
                    goLeft();
                else if(Input.GetKeyDown(KeyCode.RightArrow))
                    goRight();
            }
            if(Input.GetKeyDown(KeyCode.UpArrow)  && (IsActiveStage(TutorialManager.TutorialStage.Third) || isDown))
                goUP();
            else if(Input.GetKeyDown(KeyCode.DownArrow) && !isDown && IsActiveStage(TutorialManager.TutorialStage.Second))
                goDown();
        }
    }

    private void OnCollisionEnter(Collision other){
        Vector3 diePos = new Vector3(transform.position.x , 0.5f , transform.position.z);
        if(other.gameObject.tag == "Obstacle"){
            Instantiate(dieEffect , diePos , transform.rotation);
            // gameObject.SetActive(false);
            // levelEnded = true;
            
            // tutorialManager.ReloadStage();
            // tutorialManager.PlayerDead();
            tutorialManager.setTimer(true);
        }
    }

    private bool IsActiveStage(TutorialManager.TutorialStage stage){
        return tutorialManager.GetTutorialStage() == stage;
    }

    public void SetMove(bool val){
        move = val;
    }
}
