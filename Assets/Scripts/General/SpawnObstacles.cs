using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    public GameObject player;
    protected static Transform tf;

    public GameObject[] obstacles; //Array with type of obstacles there are
    private int lastSpawnedIndex = -1; //var to make it more random

    //Keep track of the index of the obstacle in the array
    private int obstacleIndex;

    //Variables to separate spawn times
    public float interval; 
    public float nextSpawn;

    //Variables to spawn 1 type of obstacles
    public bool onlySpawnDownObs = false;
    public bool onlySpawnMidObs = false;
    public bool onlySpawnUpObs = false;

    public GameManager gameManager;

    protected int _obstaclesSpawnedCount = 0;

    private void Start() {
        tf = gameObject.transform; 
        nextSpawn = Mathf.RoundToInt(Time.time);
    }

    private void Update() { 
        SetInterval();
    }

    protected void SetInterval(){
        if(!player.GetComponent<PlayerController>().GetLevelEnded()){
            interval = 10 / gameManager.GetVelocity();
            TimerController();
        }
    }

    protected void TimerController(){
        if (Time.time >= nextSpawn){
            nextSpawn = Mathf.RoundToInt(Time.time + interval);
            Debug.Log(nextSpawn);
            Spawn();
        }
    }

    protected void Spawn(){

        obstacleIndex = generateSpawnIndex(); //Change index or set one for testing or tutorials

        if(lastSpawnedIndex != obstacleIndex){//Obstacle A
            if(obstacleIndex == 0){

                instantiateObstacle(tf.position,tf.rotation);

            } else if(obstacleIndex == 1){ //Obstacle L

                Vector3 posL1 = new Vector3(tf.position.x , tf.position.y , -0.25f);
                Vector3 posL2 = new Vector3(tf.position.x , tf.position.y , 0.25f);
                int rndLPos = Random.Range(0 , 2);

                if(rndLPos == 0){
                    instantiateObstacle(posL1,tf.rotation);
                }else if(rndLPos == 1){
                    instantiateObstacle(posL2,tf.rotation);
                }

            } else if(obstacleIndex == 2){ //Obstacle T

                Vector3 posT1 = new Vector3(tf.position.x , tf.position.y , -0.75f);
                Vector3 posT2 = new Vector3(tf.position.x , tf.position.y , 0.75f);
                int randIndex = Random.Range(0 , 3);

                if(randIndex == 0 || randIndex == 1){
                    instantiateObstacle(posT1,tf.rotation);
                    instantiateObstacle(posT2,tf.rotation);
                } else if(randIndex == 2){
                    int rndObsPos = Random.Range(0 , 2);
                    if(rndObsPos == 0){
                        instantiateObstacle(posT1,tf.rotation);
                    }else if(rndObsPos == 1){
                        instantiateObstacle(posT2,tf.rotation);
                    }
                }
            } else if(obstacleIndex == 3 || obstacleIndex == 4 || obstacleIndex == 5){//Obstacles DL, DM, DR

                instantiateObstacle(tf.position,tf.rotation);

            }else if(obstacleIndex == 6){//Obstacle UL

                Vector3 obstaclePos1 = new Vector3(tf.position.x , 0f , -0.75f);
                Vector3 obstaclePos2 = new Vector3(tf.position.x , 0f , 0.75f);
                int randIndex = Random.Range(0 , 3);

                if(randIndex == 0 || randIndex == 1){ //Spawn two
                    instantiateObstacle(obstaclePos1,tf.rotation);
                    instantiateObstacle(obstaclePos2,tf.rotation);
                } else if(randIndex == 2){//Spawn one
                    int rndObsPos = Random.Range(0 , 2);
                    if(rndObsPos == 0){
                        instantiateObstacle(obstaclePos1,tf.rotation);//Spawn left
                    }else if(rndObsPos == 1){
                        instantiateObstacle(obstaclePos2,tf.rotation);//Spawn right
                    }
                }

            }else if(obstacleIndex == 7){ //Obstacle UM

                Vector3 obstaclePos = new Vector3(tf.position.x , -0.114f , tf.position.z);
                instantiateObstacle(obstaclePos,tf.rotation);

            }else if(obstacleIndex == 8){ //Obstacle UM2

                Vector3 obstaclePos = new Vector3(tf.position.x , 0.13f , tf.position.z);
                instantiateObstacle(obstaclePos,tf.rotation);

            }else if(obstacleIndex == 9 || obstacleIndex == 10){ //Obtacles UL2, UR2

                Vector3 obstaclePos = new Vector3(tf.position.x , 0.25f , tf.position.z);
                instantiateObstacle(obstaclePos,tf.rotation);

            }

            _obstaclesSpawnedCount++;
        }else{
            Spawn(); //Repeat if its the same index
        }
    }

    protected void SpawnSpecificObject(int objectIndex){
        obstacleIndex = objectIndex;
        instantiateObstacle(tf.position,tf.rotation);
    }
    
    protected int generateSpawnIndex(){

        if(!onlySpawnDownObs && onlySpawnMidObs && !onlySpawnUpObs) //Spawn Mid Obstacles
            return Random.Range(0 , 3);
        else if(onlySpawnDownObs && !onlySpawnMidObs && !onlySpawnUpObs)//Spawn Down Obstacles
            return Random.Range(3 , 6);
        else if(!onlySpawnDownObs && !onlySpawnMidObs && onlySpawnUpObs)//Spawn Up Obstacles
            return Random.Range(6 , 12);
        else
            return Random.Range(0 , obstacles.Length); //Spawn every type
    }

    protected void instantiateObstacle(Vector3 pos, Quaternion rot){
        Instantiate(obstacles[obstacleIndex], pos, rot);

        lastSpawnedIndex = obstacleIndex; //Update the lastSpawnedIndex
    }

    public void setSpawnType(bool spawnDown, bool spawnMid, bool spawnUp){ //For other classes, i.e tutorial
        onlySpawnDownObs = spawnDown;
        onlySpawnMidObs = spawnMid;
        onlySpawnUpObs = spawnUp;
    }

    public void ResetCount(){
        _obstaclesSpawnedCount = 0;
    }
}
