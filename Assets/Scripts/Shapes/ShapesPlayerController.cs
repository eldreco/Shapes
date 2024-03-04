using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using static ShapesManager;

public class ShapesPlayerController : PlayerController
{
    public static new ShapesPlayerController Instance;

    private MeshCollider _meshCollider;

    private void Awake() {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    private void Start() {
        Setup();
        _meshCollider = gameObject.GetComponent<MeshCollider>();
    }

    public void ChangeShape(Shape shape){
        Mesh newMesh = ShapesManager.Instance._shapeMeshMap[shape];
        _meshCollider.sharedMesh = newMesh;
        _anim.SetTrigger(shape.ToString()); //Make sure anim trigger has exact same name as shape
    }
}
