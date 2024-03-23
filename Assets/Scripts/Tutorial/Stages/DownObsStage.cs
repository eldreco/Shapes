using TutorialUtils;
using UnityEngine;
using UnityEngine.UI;
using static PlayerUtils.PlayerUtils;

public class DownObsStage : MonoBehaviour, IStage
{
    [SerializeField]private GameObject instructions;

    public void ShowStageInstructions(){
        SpawnObstacles.Instance.SetSpawnType(true, false, false);
        SpawnTutorial.Instance.IsActive = true;
        PlayerTutorialController.Instance.IsAbleTomove = false;
        gameObject.GetComponent<CanvasRenderer>().SetColor(new Color(1, 1, 1, 1));
    }

    public void BeginStage(){
        Time.timeScale = 1;
        PlayerTutorialController.Instance.IsAbleTomove = true;
        SpawnObstacles.Instance.OnEnable();
        gameObject.GetComponent<Renderer>().enabled = true;
        gameObject.GetComponent<CanvasRenderer>().SetColor(new Color(1, 1, 1, 0));
    }

    public string GetNeededMovement(){
        return VerticalPos.Down.ToString();
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