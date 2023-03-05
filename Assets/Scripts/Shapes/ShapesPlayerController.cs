using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapesPlayerController : PlayerController
{
    public static new ShapesPlayerController Instance;

    private enum Shape{SPHERE, CUBE, PYRAMID};

    [SerializeField] private Shape _activeShape;

    private void Awake() {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    private void Start() {
        Setup();
        _activeShape = Shape.CUBE;
        // ChangeToCube();
    }

    private new void Update() {
        if(_canMove){
            SwipeCheck();
            IsDownTimer(); //always checking timers
            IsUpTimer();
            PosControl();
            PCControl();
        }
    }

    private new void SwipeCheck(){
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

    private new void SwipeControl(){
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

    private new void PCControl(){
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

    private new void GoLeft(){
        _pos--;
        calculateAnimTrigger("LEFT");
    }

    private new void GoRight(){
        _pos++;
        calculateAnimTrigger("RIGHT");
    }

    private void calculateAnimTrigger(string movement){
        switch(movement){
            case "LEFT":
                switch (_activeShape){
                    case Shape.CUBE:
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
                        break;
                    case Shape.SPHERE:
                        if(_pos == 1){
                            _anim.SetTrigger("SRtoSM");
                        }else if(_pos == 0)
                            _anim.SetTrigger("SMtoSL");
                        break;
                }
                break;
            case "RIGHT":
                switch (_activeShape){
                case Shape.CUBE:
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
                    break;
                case Shape.SPHERE:
                    if(_pos == 1){
                        _anim.SetTrigger("SLtoSM");
                    }else if(_pos == 2)
                        _anim.SetTrigger("SMtoSR");
                    break;
                }
                break;
            
        }
    }

    public void ChangeToShpere(){
        if(_activeShape != Shape.SPHERE){
            _anim.SetTrigger("SphereM");
            _activeShape = Shape.SPHERE; 
        }
    }

    public void ChangeToCube(){
        if(_activeShape != Shape.CUBE){
            _anim.SetTrigger("CubeM");
            _activeShape = Shape.CUBE; 
        }
    }

    public void ChangeToPyramid(){
        if(_activeShape != Shape.PYRAMID){
            _activeShape = Shape.PYRAMID; 
        }
    }
}
