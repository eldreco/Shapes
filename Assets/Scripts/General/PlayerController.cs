using System;
using UnityEngine;
using static Utils.PlayerUtils;
using static MobileControl.MobileControl;
using TimerUtils;
using Utils;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    protected Animator anim;
    
    public PlayerState state;
    
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
        state = PlayerState.GetDefaultClassicState();
    }

    protected void Update() {
        if(CanMove){
            SwipeCheck();
            PCControl();

            if(state.VPos == VerticalPos.Down)
                timerDown.ExecuteTimer();
            else if (state.VPos == VerticalPos.Up)
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

            if(state.VPos == VerticalPos.Down) //if player is down make the player go Up after passing an obstacle
                GoUP();
            else if(state.VPos == VerticalPos.Up)
                GoDown();
                
            obsPassed = other.gameObject.name;
        }
    }

    protected void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Obstacle"))
        {
            HandlePlayerDeath();
        }
    }

    protected void HandlePlayerDeath()
    {
        Vector3 diePos = new(transform.position.x , 0.5f , transform.position.z);
        Instantiate(dieEffect, diePos, transform.rotation);
        gameObject.SetActive(false);
        GameManager.Instance.LevelEnded();
        OnPlayerDied?.Invoke();
    }

    protected void PCControl(){
        if(!GameManager.Instance.IsGamePaused){
            if(state.VPos != VerticalPos.Up){
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
                if(Length.x > SwipeDistance && state.VPos != VerticalPos.Up)
                    GoRight();
                else if( Length.x < - SwipeDistance && state.VPos != VerticalPos.Up)
                    GoLeft();
                
            } else if(xLength < yLength){
                if(Length.y > SwipeDistance)
                    GoUP();
                else if(Length.y < - SwipeDistance && state.VPos != VerticalPos.Down)
                    GoDown();
            }
        }
    }

    public void GoDown(){ 
        timerUp.ResetTimer();
        switch (state.VPos) {
            case VerticalPos.Middle:
                state.VPos = VerticalPos.Down;
                OnPlayerMovedV?.Invoke(VerticalPos.Down);
                break;
            case VerticalPos.Up:
                state.VPos = VerticalPos.Middle;
                break;
        }
        if(anim.GetFloat(ANIM_VPOS) > -1f)
            anim.SetFloat(ANIM_VPOS, anim.GetFloat(ANIM_VPOS) - 1f);
        
    }

    public void GoUP(){
        timerDown.ResetTimer();
        switch (state.VPos) {
            case VerticalPos.Middle:
                state.VPos = VerticalPos.Up;
                OnPlayerMovedV?.Invoke(VerticalPos.Down);
                break;
            case VerticalPos.Down:
                state.VPos = VerticalPos.Middle;
                break;
        }
        float animVPos = anim.GetFloat(ANIM_VPOS);
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
                state.HPos = HorizontalPos.Left;
                break;
            case 0f:
                state.HPos = HorizontalPos.Middle;
                break;
            case 1f:
                state.HPos = HorizontalPos.Right;
                break;
        }
        
        OnPlayerMovedH?.Invoke(state.HPos);
    }
}
