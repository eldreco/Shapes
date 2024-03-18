using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMenuController : MonoBehaviour
{
    private static Transform _tf;

    public GameObject[] obstacles; //Array with type of obstacles there are

    //Keep track of the index of the obstacle in the array
    private int _obstacleIndex;

    //Variables to separate spawn times
    private float _interval = 2f; 
    private float _nextSpawn = 0;

    private float _menuVelocity = 5f; 

    private void Start() {
        _tf = gameObject.transform; 
        _nextSpawn = Mathf.RoundToInt(Time.time);
        Spawn();
    }

    private void Update() { 
        TimerController();
        GameManager.Instance.ObstacleVelocity = _menuVelocity;
    }

    private void TimerController(){
        if (Time.time >= _nextSpawn){
            _nextSpawn = (Time.time + _interval);
        }
    }

    private void Spawn(){
        instantiateObstacle(_tf.position, _tf.rotation); //Spawn tutorial(index 0)
    }
    
    private void instantiateObstacle(Vector3 pos, Quaternion rot){
        Instantiate(obstacles[_obstacleIndex], pos, rot);
    }
}
