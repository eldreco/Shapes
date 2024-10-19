using System.Linq;
using General.Obstacles.Types;
using UnityEngine;
using Utils;
using static Utils.PlayerUtils;

public class ShapesPlayerController : PlayerController {
    public new static ShapesPlayerController Instance;

    private MeshCollider _meshCollider;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
        }

        Instance = this;
    }

    private new void Start() {
        Setup();
        _meshCollider = gameObject.GetComponent<MeshCollider>();
        ChangeShape(PlayerShape.Hexagon);
    }

    private new void OnCollisionEnter(Collision other) {

        // Intentionally empty
    }

    public new void OnTriggerEnter(Collider other) {

        base.OnTriggerEnter(other);

        if (other.gameObject.GetComponent<Obstacle>() == null) {
            return;
        }

        if (HasPlayerCollided(other)) {
            HandlePlayerDeath();
        }
    }

    public void ChangeShape(PlayerShape shape) {
        state = ShapesPlayerState.GetDefaultShapesState();
        var nextMesh = ShapesManager.Instance.ShapeMeshMap[shape];
        _meshCollider.sharedMesh = nextMesh;

        //Make sure anim trigger has exact same name as shape
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName(shape.ToString())) {
            anim.SetTrigger(shape.ToString());
        }
    }

    private bool HasPlayerCollided(Collider other) {

        var obstacle = other.gameObject.GetComponent<ShapesObstacle>();

        return obstacle.NonCollidingStateList.All(nonCollidingState => !nonCollidingState.Equals(state));
    }
}