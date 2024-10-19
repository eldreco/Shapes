using System.Collections.Generic;
using UnityEngine;
using static Utils.PlayerUtils;

public class ShapesManager : MonoBehaviour
{
    public static ShapesManager Instance;

    public PlayerShape ActiveShape {get; private set;}
    public Dictionary<PlayerShape, Mesh> ShapeMeshMap {get; private set;} = new();

    [SerializeField] private Mesh hexagonMesh;
    [SerializeField] private Mesh pyramidMesh;
    [SerializeField] private Mesh cylinderMesh;

    private void Awake() {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    private void Start() {
        ShapeMeshMap.Add(PlayerShape.Hexagon, hexagonMesh);
        ShapeMeshMap.Add(PlayerShape.Triangle, pyramidMesh);
        ShapeMeshMap.Add(PlayerShape.Circle, cylinderMesh);
        SetActiveShape(PlayerShape.Hexagon);
        GameManager.Instance.ActiveGameMode = GameMode.Shapes;
    }

    public void SetActiveShape(PlayerShape shape){
        ActiveShape = shape;
    }

    public void PressCircle(){
        SetActiveShape(PlayerShape.Circle);
        ShapesPlayerController.Instance.ChangeShape(PlayerShape.Circle);
    }

    public void PressHexagon(){
        SetActiveShape(PlayerShape.Hexagon);
        ShapesPlayerController.Instance.ChangeShape(PlayerShape.Hexagon);
    }

    public void PressTriangle(){
        SetActiveShape(PlayerShape.Triangle);
        ShapesPlayerController.Instance.ChangeShape(PlayerShape.Triangle);
    }
}
