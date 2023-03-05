using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    protected static Transform _tf;

    [SerializeField] private GameObject[] obstacles; //Array with type of obstacles there are
    private int _lastSpawnedIndex = -1; //var to make it more random

    //Keep track of the index of the obstacle in the array
    private int _obstacleIndex;

    //Variables to separate spawn times
    protected float _interval; 
    protected float _nextSpawn;

    //Variables to spawn 1 type of obstacles
    private bool _onlySpawnDownObs = false;
    private bool _onlySpawnMidObs = false;
    private bool _onlySpawnUpObs = false;

    protected int _obstaclesSpawnedCount = 0;

    private PlayerController _playerController;

    private void Start() {
        _tf = gameObject.transform; 
        _playerController = GameManager.Instance._playerController;
        _nextSpawn = Mathf.RoundToInt(Time.time);
    }

    private void Update() { 
        SetInterval();
    }

    protected void SetInterval(){
        if(!_playerController._levelEnded){
            _interval = 10 / GameManager.Instance._obstacleVelocity;
            TimerController();
        }
    }

    protected void TimerController(){
        if (Time.time >= _nextSpawn){
            _nextSpawn = Mathf.RoundToInt(Time.time + _interval);
            Spawn();
        }
    }

    protected void Spawn(){

        _obstacleIndex = generateSpawnIndex(); //Change index or set one for testing or tutorials

        if(_lastSpawnedIndex != _obstacleIndex){//Obstacle A
            if(_obstacleIndex == 0){

                instantiateObstacle(_tf.position,_tf.rotation);

            } else if(_obstacleIndex == 1){ //Obstacle L

                Vector3 posL1 = new Vector3(_tf.position.x , _tf.position.y , -0.25f);
                Vector3 posL2 = new Vector3(_tf.position.x , _tf.position.y , 0.25f);
                int rndLPos = Random.Range(0 , 2);

                if(rndLPos == 0){
                    instantiateObstacle(posL1,_tf.rotation);
                }else if(rndLPos == 1){
                    instantiateObstacle(posL2,_tf.rotation);
                }

            } else if(_obstacleIndex == 2){ //Obstacle T

                Vector3 posT1 = new Vector3(_tf.position.x , _tf.position.y , -0.75f);
                Vector3 posT2 = new Vector3(_tf.position.x , _tf.position.y , 0.75f);
                int randIndex = Random.Range(0 , 3);

                if(randIndex == 0 || randIndex == 1){
                    instantiateObstacle(posT1,_tf.rotation);
                    instantiateObstacle(posT2,_tf.rotation);
                } else if(randIndex == 2){
                    int rndObsPos = Random.Range(0 , 2);
                    if(rndObsPos == 0){
                        instantiateObstacle(posT1,_tf.rotation);
                    }else if(rndObsPos == 1){
                        instantiateObstacle(posT2,_tf.rotation);
                    }
                }
            } else if(_obstacleIndex == 3 || _obstacleIndex == 4 || _obstacleIndex == 5){//Obstacles DL, DM, DR

                instantiateObstacle(_tf.position,_tf.rotation);

            }else if(_obstacleIndex == 6){//Obstacle UL

                Vector3 obstaclePos1 = new Vector3(_tf.position.x , 0f , -0.75f);
                Vector3 obstaclePos2 = new Vector3(_tf.position.x , 0f , 0.75f);
                int randIndex = Random.Range(0 , 3);

                if(randIndex == 0 || randIndex == 1){ //Spawn two
                    instantiateObstacle(obstaclePos1,_tf.rotation);
                    instantiateObstacle(obstaclePos2,_tf.rotation);
                } else if(randIndex == 2){//Spawn one
                    int rndObsPos = Random.Range(0 , 2);
                    if(rndObsPos == 0){
                        instantiateObstacle(obstaclePos1,_tf.rotation);//Spawn left
                    }else if(rndObsPos == 1){
                        instantiateObstacle(obstaclePos2,_tf.rotation);//Spawn right
                    }
                }

            }else if(_obstacleIndex == 7){ //Obstacle UM

                Vector3 obstaclePos = new Vector3(_tf.position.x , -0.114f , _tf.position.z);
                instantiateObstacle(obstaclePos,_tf.rotation);

            }else if(_obstacleIndex == 8){ //Obstacle UM2

                Vector3 obstaclePos = new Vector3(_tf.position.x , 0.13f , _tf.position.z);
                instantiateObstacle(obstaclePos,_tf.rotation);

            }else if(_obstacleIndex == 9 || _obstacleIndex == 10){ //Obtacles UL2, UR2

                Vector3 obstaclePos = new Vector3(_tf.position.x , 0.25f , _tf.position.z);
                instantiateObstacle(obstaclePos,_tf.rotation);

            }

            _obstaclesSpawnedCount++;
        }else{
            Spawn(); //Repeat if its the same index
        }
    }

    protected void SpawnSpecificObject(int objectIndex){
        _obstacleIndex = objectIndex;
        instantiateObstacle(_tf.position,_tf.rotation);
    }
    
    protected int generateSpawnIndex(){

        if(!_onlySpawnDownObs && _onlySpawnMidObs && !_onlySpawnUpObs) //Spawn Mid Obstacles
            return Random.Range(0 , 3);
        else if(_onlySpawnDownObs && !_onlySpawnMidObs && !_onlySpawnUpObs)//Spawn Down Obstacles
            return Random.Range(3 , 6);
        else if(!_onlySpawnDownObs && !_onlySpawnMidObs && _onlySpawnUpObs)//Spawn Up Obstacles
            return Random.Range(6 , 12);
        else
            return Random.Range(0 , obstacles.Length); //Spawn every type
    }

    protected void instantiateObstacle(Vector3 pos, Quaternion rot){
        Instantiate(obstacles[_obstacleIndex], pos, rot);

        _lastSpawnedIndex = _obstacleIndex; //Update the lastSpawnedIndex
    }

    public void setSpawnType(bool spawnDown, bool spawnMid, bool spawnUp){ //For other classes, i.e tutorial
        _onlySpawnDownObs = spawnDown;
        _onlySpawnMidObs = spawnMid;
        _onlySpawnUpObs = spawnUp;
    }

    public void ResetCount(){
        _obstaclesSpawnedCount = 0;
    }
}
