using UnityEngine;

public class ObstacleController : MonoBehaviour
{   
    protected Rigidbody rb;
    protected Animator anim;

    protected void Start() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    protected void FixedUpdate() {
        MoveObstacle(GameManager.Instance.ObstacleVelocity);
    }

    protected void MoveObstacle(float speed){
        rb.MovePosition(transform.position + speed * Time.deltaTime * transform.right);
    }
}
