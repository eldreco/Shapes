using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{   
    protected Rigidbody rb;
    protected Animator anim;

    protected GameObject gameManager;

    protected void Start() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>().gameObject;
    }

    protected void Update() {
        MoveObstacle(gameManager.GetComponent<GameManager>().GetVelocity());
    }

    protected void MoveObstacle(float speed){
        rb.velocity = new Vector3(speed , 0 , 0);
    }
}
