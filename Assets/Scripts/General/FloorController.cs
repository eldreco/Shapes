using UnityEngine;
using static Utils.PlayerUtils;

public class FloorController : MonoBehaviour
{
    public static FloorController Instance;

    [SerializeField] private Renderer middleRenderer;
    [SerializeField] private Renderer leftRenderer;
    [SerializeField] private Renderer rightRenderer;

    private Color blackColor = new(0.15f, 0.15f, 0.15f);

    private void Awake() {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    private void Start() {
        PlayerController.OnPlayerMovedH += PlayerPosCheck;
    }

    private void OnDisable() {
        PlayerController.OnPlayerMovedH -= PlayerPosCheck;
    }

    private void PlayerPosCheck(HorizontalPos pos){
        leftRenderer.material.color = pos == HorizontalPos.Left ? Color.white : blackColor;
        middleRenderer.material.color = pos == HorizontalPos.Middle ? Color.white : blackColor;
        rightRenderer.material.color = pos == HorizontalPos.Right ? Color.white : blackColor;
    }

    public void Reset(){
        leftRenderer.material.color = blackColor;
        middleRenderer.material.color = Color.white;
        rightRenderer.material.color = blackColor;
    }
}
