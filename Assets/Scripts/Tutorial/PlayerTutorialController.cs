using UnityEngine;

public class PlayerTutorialController : MonoBehaviour
{
    public static PlayerTutorialController Instance;

    public bool IsAbleTomove {get; set;}

    private void Awake() {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    private void Start() {
        IsAbleTomove = true;
    }

    //New Move to ignore parent one, checks the tutorialStage
    public void Move(Vector2 startPos, Vector2 endPos){
        Vector2 Length = startPos - endPos;
        float xLength = Mathf.Abs(Length.x);
        float yLength = Mathf.Abs(Length.y);

        if(!GameManager.Instance.IsGamePaused){
            if(xLength > yLength){
                if(Length.x > 0 && !PlayerController.Instance.IsUp && IsAbleTomove)
                PlayerController.Instance.GoRight();
                else if( Length.x < 0 && !PlayerController.Instance.IsUp && IsAbleTomove)
                PlayerController.Instance.GoLeft();    
            } else if(xLength < yLength){
                if(Length.y > 0 && (PlayerController.Instance.IsDown))
                PlayerController.Instance.GoUP();
                else if(Length.y < 0 && !PlayerController.Instance.IsDown)
                PlayerController.Instance.GoDown();     
            }
        }
    }

    private void PCControl(){
        if(!GameManager.Instance.IsGamePaused){
            if(!PlayerController.Instance.IsUp && IsAbleTomove){
                if(Input.GetKeyDown(KeyCode.LeftArrow))
                PlayerController.Instance.GoLeft();
                else if(Input.GetKeyDown(KeyCode.RightArrow))
                PlayerController.Instance.GoRight();
            }
            if(Input.GetKeyDown(KeyCode.UpArrow)  && ( PlayerController.Instance.IsDown))
            PlayerController.Instance.GoUP();
            else if(Input.GetKeyDown(KeyCode.DownArrow) && !PlayerController.Instance.IsDown)
            PlayerController.Instance.GoDown();
        }
    }

    private void OnCollisionEnter(Collision other){
        Vector3 diePos = new Vector3(transform.position.x , 0.5f , transform.position.z);
        if(other.gameObject.tag == "Obstacle"){
            Instantiate(PlayerController.Instance.dieEffect , diePos , transform.rotation);
            TutorialManager.Instance.CheckTimer = true;
            TutorialManager.Instance.ManagePlayerDied();
        }
    }
}
