using System.Collections.Generic;
using UnityEngine;

public class ShapesManager : MonoBehaviour
{
    public static ShapesManager Instance;

    public enum Shape{Cylinder, Hexagon, Pyramid};
    public Shape ActiveShape {get; private set;}
    public Dictionary<Shape, Mesh> ShapeMeshMap {get; private set;} = new();

    [SerializeField] private Mesh hexagonMesh;
    [SerializeField] private Mesh pyramidMesh;
    [SerializeField] private Mesh cylinderMesh;

    private void Awake() {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    private void Start() {
        ShapeMeshMap.Add(Shape.Hexagon, hexagonMesh);
        ShapeMeshMap.Add(Shape.Pyramid, pyramidMesh);
        ShapeMeshMap.Add(Shape.Cylinder, cylinderMesh);
        SetActiveShape(Shape.Hexagon);
        GameManager.Instance.ActiveGameMode = GameMode.Shapes;
    }

    public void SetActiveShape(Shape shape){
        ActiveShape = shape;
    }

    public void PressCircle(){
        SetActiveShape(Shape.Cylinder);
        ShapesPlayerController.Instance.ChangeShape(Shape.Cylinder);
    }

    public void PressHexagon(){
        SetActiveShape(Shape.Hexagon);
        ShapesPlayerController.Instance.ChangeShape(Shape.Hexagon);
    }

    public void PressTriangle(){
        SetActiveShape(Shape.Pyramid);
        ShapesPlayerController.Instance.ChangeShape(Shape.Pyramid);
    }
}
