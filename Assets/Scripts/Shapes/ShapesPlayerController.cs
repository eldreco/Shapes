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
    }

    public void ChangeShape(Shape shape){
        Mesh newMesh = ShapesManager.Instance.ShapeMeshMap[shape];
        _meshCollider.sharedMesh = newMesh;
        anim.SetTrigger(shape.ToString()); //Make sure anim trigger has exact same name as shape
    }
}
