using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private Rigidbody _rb;
    protected Animator _anim;

    private GameObject _playersBack;

    protected bool _isUp;
    protected bool _isDown;
    protected bool _gamePaused;
    protected int _pos = 1;

    //Check Position of the swipe to do the check
    protected float _swipeStartTime;
    protected float _swipeEndTime;
    protected Vector2 _swipeStartPos;
    protected Vector2 _swipeEndPos;
    protected int _swipeDistance;

    [SerializeField]protected GameObject _dieEffect;

    public bool _levelEnded {get; private set;}

    private float _timerDown = 0;
    private float _timerDownValue = 1f;
    private float _timerUp = 0;
    private float _timerUpValue = 1f;

    private string _obsPassed = "";

    private int _score = 0;

    protected bool _canMove;

    private void Awake() {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    private void Start() {
        Setup();
    }

    protected void Setup(){
        _canMove = true;
        _rb = GetComponent<Rigidbody>();
        _playersBack = gameObject.GetComponent<GameObject>();
        _anim = GetComponent<Animator>();
        _levelEnded = false;
        _timerDown = _timerDownValue;
        _timerUp = _timerUpValue;

        if(SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Main Menu"))
            _swipeDistance = 25;
        else
            _swipeDistance = 5;
    }

    protected void Update() {
        if(_canMove){
            SwipeCheck();
            IsDownTimer(); //always checking timers
            IsUpTimer();
            PosControl();
            PCControl();
        }
    }

    public void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Obstacle"){
            _timerDown = _timerDownValue; //Reset timer if it starts passing an obstacle
            _timerUp = _timerUpValue;
        }
    }

    public void OnTriggerExit(Collider other) { 
        if(other.gameObject.tag == "Obstacle" && other.gameObject.name != _obsPassed){ //Second cond in order not to get 2 points for passing complex obs
            _score++;
            if(_isDown) //if player is down make the player go Up after passing an obstacle
                GoUP();
            else if(_isUp){
                GoDown();
                _timerUp = _timerUpValue;
            }
        }
        _obsPassed = other.gameObject.name;
    }

    private void OnCollisionEnter(Collision other) {
        Vector3 diePos = new Vector3(transform.position.x , 0.5f , transform.position.z);
        if(other.gameObject.tag == "Obstacle"){
            Instantiate(_dieEffect , diePos , transform.rotation);
            gameObject.SetActive(false);
            _levelEnded = true;
        }
    }

    protected void SwipeCheck(){
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began){

                _swipeStartTime = Time.time;
                _swipeStartPos = touch.position;

            } else if(touch.phase == TouchPhase.Ended){

                _swipeEndTime = Time.time;
                _swipeEndPos = touch.position;
                SwipeControl();
            }
        }
    } 

    protected void PCControl(){
        if(!_gamePaused){
            if(!_isUp){
                if(Input.GetKeyDown(KeyCode.LeftArrow))
                    GoLeft();
                else if(Input.GetKeyDown(KeyCode.RightArrow))
                    GoRight();
            }
            if(Input.GetKeyDown(KeyCode.UpArrow))
                GoUP();
            else if(Input.GetKeyDown(KeyCode.DownArrow))
                GoDown();
        }
    }

    public void SwipeControl(){
        Vector2 Length = _swipeEndPos - _swipeStartPos;
        float xLength = Mathf.Abs(Length.x);
        float yLength = Mathf.Abs(Length.y);

        if(!_gamePaused){
            if(xLength > yLength){
                if(Length.x > _swipeDistance && !_isUp){
                    GoRight();
                } else if( Length.x < -_swipeDistance && !_isUp){
                    GoLeft();
                }
            } else if(xLength < yLength){
                if(Length.y > _swipeDistance){
                    GoUP();
                } else if(Length.y < -_swipeDistance && !_isDown){
                    GoDown();
                }
            }
        }
    }

    protected void GoDown(){ //When its up it goes down with isUpTimer()
        if(!_isUp){ //if its in the middle
            _isDown = true;
            if(_pos == 0)
                _anim.SetTrigger("LtoD");
            else if(_pos == 1)
                _anim.SetTrigger("MtoD");
            else
                _anim.SetTrigger("RtoD");
        } else if(_isUp){ //if its up
            _isUp = false;
            if(_pos == 0)
                _anim.SetTrigger("UtoL");
            else if(_pos == 1)
                _anim.SetTrigger("UtoM");
            else
                _anim.SetTrigger("UtoR");
        }
    }

    protected void GoUP(){

        if(_isDown){
            _isDown = false;
            if(_pos == 0)
                _anim.SetTrigger("DtoL");
            else if(_pos == 1)
                _anim.SetTrigger("DtoM");
            else
                _anim.SetTrigger("DtoR");
        }
        else if(!_isUp){ //if its middle
            if(_pos == 0)
                _anim.SetTrigger("LtoU");
            else if(_pos == 1)
                _anim.SetTrigger("MtoU");
            else
                _anim.SetTrigger("RtoU");
            _isUp = true;
        }
    }

    protected void GoLeft(){ 
        _pos--;
        if(_isDown){
            if(_pos == 1){
                _anim.SetTrigger("RDtoMD");
            }else if(_pos == 0)
                _anim.SetTrigger("MDtoLD");
        } else if(!_isUp){
            if(_pos == 1){
                _anim.SetTrigger("RtoM");
            }else if(_pos == 0)
                _anim.SetTrigger("MtoL");
        }
    }

    protected void GoRight(){ 
        _pos++;
        if(_isDown){
            if(_pos == 1){
                _anim.SetTrigger("LDtoMD");
            }else if(_pos == 2)
                _anim.SetTrigger("MDtoRD");
        } else if(!_isUp){
            if(_pos == 1){
                _anim.SetTrigger("LtoM");
            }else if(_pos == 2)
                _anim.SetTrigger("MtoR");
        }
    }

    protected void IsDownTimer(){
        if(_isDown){
            _timerDown -= Time.deltaTime;
            if(_timerDown <= 0){
                GoUP(); //when down you can go to middle before the full animation
                _timerDown = _timerDownValue;
            }
        }
    }

    protected void IsUpTimer(){
        if(_isUp){
            _timerUp -= Time.deltaTime;
            if(_timerUp <= 0){
                GoDown();
                _timerUp = _timerUpValue;
            }
        }
    }

    public void PosControl(){ //So if you swipe further it doesn't count
        if(_pos < 0)
            _pos = 0;
        else if(_pos > 2)
            _pos = 2;
    }

    public void setPause(bool paused){
        _gamePaused = paused;
    }
    
    public int getScore(){
        return _score;
    }

    public int getPos(){
        return _pos;
    }

    public void setPos(int position){
        _pos = position;
    }

    public bool getIsDown(){
        return _isDown;
    }

    public bool getIsUp(){
        return _isUp;
    }

    public void SetCanMove(bool value){
        _canMove = value;
    }
}
