using UnityEngine;
using static ShapesManager;

public class ShapesPlayerController : PlayerController
{
    public static new ShapesPlayerController Instance;

    private MeshCollider _meshCollider;

    private void Awake() {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    private new void Start() {
        Setup();
        _meshCollider = gameObject.GetComponent<MeshCollider>();
        ChangeShape(Shape.Hexagon);
    }

    public void ChangeShape(Shape shape){
        Mesh nextMesh = ShapesManager.Instance.ShapeMeshMap[shape];
        _meshCollider.sharedMesh = nextMesh;
        //Make sure anim trigger has exact same name as shape
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName(shape.ToString()))
            anim.SetTrigger(shape.ToString());
    }
}
