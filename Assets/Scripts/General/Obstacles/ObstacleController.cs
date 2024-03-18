using UnityEngine;

public class ObstacleController : MonoBehaviour
{   
    protected Rigidbody rb;
    protected Animator anim;

    protected void Start() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    protected void Update() {
        MoveObstacle(GameManager.Instance.ObstacleVelocity);
    }

    protected void MoveObstacle(float speed){
        transform.position += speed * Time.deltaTime * transform.right;
    }
}
