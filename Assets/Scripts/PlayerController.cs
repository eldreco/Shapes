using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    protected Animator anim;

    private GameObject mainMenuManager;
    private MainMenuManager mMenuManager;

    private GameObject playersBack;

    public bool isUp;
    public bool isDown;

    public bool gamePaused;

    public int pos = 1;

    //Check Position of the swipe to do the check
    private float swipeStartTime;
    private float swipeEndTime;
    protected Vector2 swipeStartPos;
    protected Vector2 swipeEndPos;
    private int swipeDistance;

    public GameObject dieEffect;

    private bool levelEnded;

    private float timerDown = 0;
    public float timerDownValue = 1f;
    public float timerUp = 0;
    public float timerUpValue = 1f;

    private string obsPassed = "";

    public int score = 0;

    private bool canMove;

    private void Start() {
        canMove = true;
        rb = GetComponent<Rigidbody>();
        playersBack = gameObject.GetComponent<GameObject>();
        anim = GetComponent<Animator>();
        levelEnded = false;
        timerDown = timerDownValue;
        timerUp = timerUpValue;

        if(SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Main Menu"))
            swipeDistance = 25;
        else
            swipeDistance = 5;
        // mainMenuManager = GameObject.Find("Main Menu Manager");
        // mMenuManager = mainMenuManager.GetComponent<MainMenuManager>();
    }

    private void Update() {
        if(canMove){
            SwipeCheck();
            IsDownTimer(); //always checking timers
            IsUpTimer();
            posControl();
            PCControl();
        }
    }

    public void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Obstacle"){
            timerDown = timerDownValue; //Reset timer if it starts passing an obstacle
            timerUp = timerUpValue;
            Debug.Log(other.name);
        }else if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Main Menu")) {
            // mMenuManager.playerEnterTrigger(other); //Call function from MainMenuManager
        }
    }

    public void OnTriggerExit(Collider other) { 
        if(other.gameObject.tag == "Obstacle" && other.gameObject.name != obsPassed){ //Second cond in order not to get 2 points for passing complex obs
            score++;
            if(isDown) //if player is down make the player go Up after passing an obstacle
                goUP();
            else if(isUp){
                goDown();
                timerUp = timerUpValue;
            }
        }else if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Main Menu")) {
            // mMenuManager.playerExitTrigger(other); //Call function from MainMenuManager
        }
        obsPassed = other.gameObject.name;
    }

    private void OnCollisionEnter(Collision other) {
        Vector3 diePos = new Vector3(transform.position.x , 0.5f , transform.position.z);
        if(other.gameObject.tag == "Obstacle"){
            Instantiate(dieEffect , diePos , transform.rotation);
            gameObject.SetActive(false);
            levelEnded = true;
        }
    }
    protected void SwipeCheck(){
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began){

                swipeStartTime = Time.time;
                swipeStartPos = touch.position;

            } else if(touch.phase == TouchPhase.Ended){

                swipeEndTime = Time.time;
                swipeEndPos = touch.position;
                SwipeControl();
            }
        }
    } 

    protected void PCControl(){
        if(!gamePaused){
            if(!isUp){
                if(Input.GetKeyDown(KeyCode.LeftArrow))
                    goLeft();
                else if(Input.GetKeyDown(KeyCode.RightArrow))
                    goRight();
            }
            if(Input.GetKeyDown(KeyCode.UpArrow))
                goUP();
            else if(Input.GetKeyDown(KeyCode.DownArrow))
                goDown();
        }
    }

    public void SwipeControl(){
        Vector2 Length = swipeEndPos - swipeStartPos;
        float xLength = Mathf.Abs(Length.x);
        float yLength = Mathf.Abs(Length.y);
        Debug.Log(swipeStartPos+ " ; " + swipeEndPos + " ; " + xLength + " ; " + yLength);

        if(!gamePaused){
            if(xLength > yLength){
                if(Length.x > swipeDistance && !isUp){
                    goRight();
                } else if( Length.x < -swipeDistance && !isUp){
                    goLeft();
                }
            } else if(xLength < yLength){
                if(Length.y > swipeDistance){
                    goUP();
                } else if(Length.y < -swipeDistance && !isDown){
                    goDown();
                }
            }
        }
    }
 
    public bool GetLevelEnded(){
        return levelEnded;
    }

    protected void goDown(){ //When its up it goes down with isUpTimer()
        if(!isUp){ //if its in the middle
            isDown = true;
            if(pos == 0)
                anim.SetTrigger("LtoD");
            else if(pos == 1)
                anim.SetTrigger("MtoD");
            else
                anim.SetTrigger("RtoD");
        } else if(isUp){ //if its up
            isUp = false;
            if(pos == 0)
                anim.SetTrigger("UtoL");
            else if(pos == 1)
                anim.SetTrigger("UtoM");
            else
                anim.SetTrigger("UtoR");
        }
    }

    protected void goUP(){

        if(isDown){
            isDown = false;
            if(pos == 0)
                anim.SetTrigger("DtoL");
            else if(pos == 1)
                anim.SetTrigger("DtoM");
            else
                anim.SetTrigger("DtoR");

        }
        else if(!isUp){ //if its middle
            if(pos == 0)
                anim.SetTrigger("LtoU");
            else if(pos == 1)
                anim.SetTrigger("MtoU");
            else
                anim.SetTrigger("RtoU");
            isUp = true;
        }
    }

    protected void IsDownTimer(){
        if(isDown){
            timerDown -= Time.deltaTime;
            if(timerDown <= 0){
                goUP(); //when down you can go to middle before the full animation
                timerDown = timerDownValue;
            }
        }
    }

    protected void IsUpTimer(){
        if(isUp){
            timerUp -= Time.deltaTime;
            if(timerUp <= 0){
                goDown();
                timerUp = timerUpValue;
            }
        }
    }

    protected void goLeft(){ 
        pos--;
        if(isDown){
            if(pos == 1){
                anim.SetTrigger("RDtoMD");
            }else if(pos == 0)
                anim.SetTrigger("MDtoLD");
        } else if(!isUp){
            if(pos == 1){
                anim.SetTrigger("RtoM");
            }else if(pos == 0)
                anim.SetTrigger("MtoL");
        }
    }

    protected void goRight(){ 
        pos++;
        if(isDown){
            if(pos == 1){
                anim.SetTrigger("LDtoMD");
            }else if(pos == 2)
                anim.SetTrigger("MDtoRD");
        } else if(!isUp){
            if(pos == 1){
                anim.SetTrigger("LtoM");
            }else if(pos == 2)
                anim.SetTrigger("MtoR");
        }
    }

    public void posControl(){ //So if you swipe further it doesn't count
        if(pos < 0)
            pos = 0;
        else if(pos > 2)
            pos = 2;
    }

    public void setPause(bool paused){
        gamePaused = paused;
    }
    
    public int getScore(){
        return score;
    }

    public int getPos(){
        return pos;
    }

    public void setPos(int position){
        pos = position;
    }

    public bool getIsDown(){
        return isDown;
    }

    public bool getIsUp(){
        return isUp;
    }

    public void SetCanMove(bool value){
        canMove = value;
    }
}
