using TutorialUtils;
using UnityEngine;
using UnityEngine.UI;
using static PlayerUtils.PlayerUtils;

public class MainStage : MonoBehaviour, IStage
{

    [SerializeField]private GameObject instructions;
    
    public void ShowStageInstructions(){
        Instantiate(instructions);
        Debug.Log("Main Stage Instructions");
        SpawnObstacles.Instance.SetSpawnType(false, true, false);
        PlayerTutorialController.Instance.IsAbleTomove = false;
        ShowInstructions();
        Time.timeScale = 0;
    }

    public void BeginStage(){
        Debug.Log("Main Stage Begined");
        Time.timeScale = 1;
        GameManager.Instance.SetBaseVelocity();
        SpawnTutorial.Instance.IsActive = true;
        GameManager.Instance.IsLevelEnded = false;
        HideInstructions();        
    }

    public string GetNeededMovement(){
        return HorizontalPos.MIDDLE.ToString();
    }

    public void ShowInstructions(){
        Color color = gameObject.GetComponent<Image>().color;
        Color colorShown = new(color.r, color.g, color.b, 1);
        gameObject.GetComponent<Image>().color = colorShown;
        instructions.SetActive(true);
        instructions.transform.SetParent(TutorialCanvasController.Instance.transform);
    }

    public void HideInstructions(){
        Color color = gameObject.GetComponent<Image>().color;
        Color colorHidden = new(color.r, color.g, color.b, 0);
        gameObject.GetComponent<Image>().color = colorHidden;
        instructions.SetActive(false);
    }
}