﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{   
    protected Rigidbody _rb;
    protected Animator _anim;

    protected void Start() {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    protected void Update() {
        MoveObstacle(GameManager.Instance._obstacleVelocity);
    }

    protected void MoveObstacle(float speed){
        _rb.velocity = new Vector3(speed , 0 , 0);
    }
}