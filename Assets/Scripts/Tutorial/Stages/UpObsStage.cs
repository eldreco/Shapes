using TutorialUtils;
using UnityEngine;
using UnityEngine.UI;
using static Utils.PlayerUtils;

public class UpObsStage : MonoBehaviour, IStage
{
    [SerializeField]private GameObject instructions;

    public void ShowStageInstructions(){
        SpawnObstacles.Instance.SetSpawnType(false, false, true);
        SpawnTutorial.Instance.IsActive = true;
        gameObject.GetComponent<CanvasRenderer>().SetColor(new Color(1, 1, 1, 1));
    }

    public void BeginStage(){
        PlayerTutorialController.Instance.IsAbleTomove = true;
        Time.timeScale = 1;
        SpawnObstacles.Instance.OnEnable();
        gameObject.GetComponent<CanvasRenderer>().SetColor(new Color(1, 1, 1, 0));
    }

    public string GetNeededMovement(){
        return VerticalPos.Up.ToString();
    }

    void IStage.ShowInstructions(){
        Color color = gameObject.GetComponent<Image>().color;
        Color colorShown = new(color.r, color.g, color.b, 1);
        gameObject.GetComponent<Image>().color = colorShown;
        instructions.SetActive(false);
    }

    void IStage.HideInstructions(){
        Color color = gameObject.GetComponent<Image>().color;
        Color colorHidden = new(color.r, color.g, color.b, 0);
        gameObject.GetComponent<Image>().color = colorHidden;
        instructions.SetActive(false);
    }
}