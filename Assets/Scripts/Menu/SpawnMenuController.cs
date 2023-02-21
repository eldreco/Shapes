using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMenuController : MonoBehaviour
{
    public GameObject player;
    private static Transform tf;
    public GameManager gameManager;

    public GameObject[] obstacles; //Array with type of obstacles there are

    //Keep track of the index of the obstacle in the array
    public int obstacleIndex;

    //Variables to separate spawn times
    public float interval = 2f; 
    public float nextSpawn = 0;

    public float menuVelocity = 5f; //Changed here or in editor

    private void Start() {
        tf = gameObject.transform; 
        nextSpawn = Mathf.RoundToInt(Time.time);

        Spawn();
    }

    private void Update() { 
        TimerController();
        gameManager.SetVelocity(menuVelocity);
    }

    private void TimerController(){
        if (Time.time >= nextSpawn){
            nextSpawn = (Time.time + interval);
            // Spawn();
        }
    }

    private void Spawn(){
        instantiateObstacle(tf.position, tf.rotation); //Spawn tutorial(index 0)
    }
    


    private void instantiateObstacle(Vector3 pos, Quaternion rot){
        Instantiate(obstacles[obstacleIndex], pos, rot);
    }
}
