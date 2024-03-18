using System;
using UnityEngine;
using static PlayerUtils.PlayerUtils;
using static MobileControl.MobileControl;
using TimerUtils;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    protected Animator anim;

    public bool IsUp {get; set;} //Refactor isUp and isDown to use new VerticalPos
    public bool IsDown {get; set;}
    public HorizontalPos HPos {get; set;} = HorizontalPos.MIDDLE;

    [SerializeField] public GameObject dieEffect;

    private string obsPassed = "";

    public bool CanMove {get; set;}

    public static event Action<HorizontalPos> OnPlayerMovedH;
    public static event Action<VerticalPos> OnPlayerMovedV;
    public static event Action OnPlayerDied;
    public static Action OnObstaclePassed;

    private Timer timerUp;
    private Timer timerDown;

    private void Awake() {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    protected void OnEnable() {
        OnSwipe += Move;
        timerDown = new(1f);
        timerUp = new(1f);
        timerDown.OnTimerFinished += GoUP;
        timerUp.OnTimerFinished += GoDown;
    }

    private void OnDisable() {
        OnSwipe -= Move;
        timerDown.OnTimerFinished -= GoUP;
        timerUp.OnTimerFinished -= GoDown;
    }

    protected void Start() {
        Setup();
    }

    protected void Setup(){
        CanMove = true;
        anim = GetComponent<Animator>();
    }

    protected void Update() {
        if(CanMove){
            SwipeCheck();
            PCControl();

            if(IsDown)
                timerDown.ExecuteTimer();
            else if (IsUp)
                timerUp.ExecuteTimer();
        }
    }

    public void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Obstacle")){
            timerDown.ResetTimer();
            timerUp.ResetTimer();
        }
    }

    public void OnTriggerExit(Collider other) { 
        if(other.gameObject.CompareTag("Obstacle")){

            if(other.gameObject.name != obsPassed) //in order not to get 2 points for passing complex obs
                OnObstaclePassed?.Invoke();

            if(IsDown) //if player is down make the player go Up after passing an obstacle
                GoUP();
            else if(IsUp)
                GoDown();
                
            obsPassed = other.gameObject.name;
        }
    }

    private void OnCollisionEnter(Collision other) {
        Vector3 diePos = new(transform.position.x , 0.5f , transform.position.z);
        if(other.gameObject.CompareTag("Obstacle")){
            Instantiate(dieEffect, diePos, transform.rotation);
            gameObject.SetActive(false);
            GameManager.Instance.LevelEnded();
            OnPlayerDied?.Invoke();
        }
    }

    protected void PCControl(){
        if(!GameManager.Instance.IsGamePaused){
            if(!IsUp){
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

    public void Move(Vector2 startPos , Vector2 endPos){
        Vector2 Length = endPos - startPos;
        float xLength = Mathf.Abs(Length.x);
        float yLength = Mathf.Abs(Length.y);

        if(!GameManager.Instance.IsGamePaused){
            if(xLength > yLength){
                if(Length.x > SwipeDistance && !IsUp)
                    GoRight();
                else if( Length.x < - SwipeDistance && !IsUp)
                    GoLeft();
                
            } else if(xLength < yLength){
                if(Length.y > SwipeDistance)
                    GoUP();
                else if(Length.y < - SwipeDistance && !IsDown)
                    GoDown();
            }
        }
    }

    public void GoDown(){ 
        IsUp = false;
        timerUp.ResetTimer();
        float vPos = anim.GetFloat(ANIM_VPOS);
        if(vPos == 0f){
            IsDown = true;
            OnPlayerMovedV?.Invoke(VerticalPos.DOWN);
        }
        if(vPos > -1f)
            anim.SetFloat(ANIM_VPOS, vPos - 1f);
        
    }

    public void GoUP(){
        IsDown = false;
        timerDown.ResetTimer();
        float vPos = anim.GetFloat(ANIM_VPOS);
        if(vPos == 0f){
            IsUp = true;
            OnPlayerMovedV?.Invoke(VerticalPos.DOWN);
        }
        if(anim.GetFloat(ANIM_VPOS) < 1f)
            anim.SetFloat(ANIM_VPOS, anim.GetFloat(ANIM_VPOS) + 1f);
    }

    public void GoLeft(){ 
        if(anim.GetFloat(ANIM_HPOS) > -1f)
            anim.SetFloat(ANIM_HPOS, anim.GetFloat(ANIM_HPOS) - 1f);
        UpdateHPos();
    }

    public void GoRight(){ 
        if(anim.GetFloat(ANIM_HPOS) < 1f)
            anim.SetFloat(ANIM_HPOS, anim.GetFloat(ANIM_HPOS) + 1f);
        UpdateHPos();
    }

    private void UpdateHPos(){
        switch (anim.GetFloat(ANIM_HPOS))
        {
            case -1f:
                HPos = HorizontalPos.LEFT;
                break;
            case 0f:
                HPos = HorizontalPos.MIDDLE;
                break;
            case 1f:
                HPos = HorizontalPos.RIGHT;
                break;
        }
        
        OnPlayerMovedH?.Invoke(HPos);
    }
}
