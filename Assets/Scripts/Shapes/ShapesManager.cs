using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapesManager : MonoBehaviour
{
    public static ShapesManager Instance;

    public enum Shape{CYLINDER, CUBE, PYRAMID};
    public Shape _activeShape {get; private set;}
    public Dictionary<Shape, Mesh> _shapeMeshMap {get; private set;} = new();

    [SerializeField] private Mesh cubeMesh;
    [SerializeField] private Mesh pyramidMesh;
    [SerializeField] private Mesh cylinderMesh;

    private void Awake() {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    private void Start() {
        _shapeMeshMap.Add(Shape.CUBE, cubeMesh);
        _shapeMeshMap.Add(Shape.PYRAMID, pyramidMesh);
        _shapeMeshMap.Add(Shape.CYLINDER, cylinderMesh);
        SetActiveShape(Shape.CUBE);
    }

    public void SetActiveShape(Shape shape){
        _activeShape = shape;
    }

    public void PressCircle(){
        SetActiveShape(Shape.CYLINDER);
        ShapesPlayerController.Instance.ChangeShape(Shape.CYLINDER);
    }

    public void PressSquare(){
        SetActiveShape(Shape.CUBE);
        ShapesPlayerController.Instance.ChangeShape(Shape.CUBE);
    }

    public void PressTriangle(){
        SetActiveShape(Shape.PYRAMID);
        ShapesPlayerController.Instance.ChangeShape(Shape.PYRAMID);
    }
}
