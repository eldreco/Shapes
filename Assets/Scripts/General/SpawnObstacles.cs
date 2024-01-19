using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SpawnObstacles : MonoBehaviour
{
    protected static Transform _tf;

    [SerializeField] private GameObject[] obstacles; //Array with type of obstacles there are
    private int _lastSpawnedIndex = -1; //var to make it more random

    //Keep track of the index of the obstacle in the array
    private int obstacleIndex;

    //Variables to separate spawn times
    protected float _interval; 
    protected float _nextSpawn;

    //Variables to spawn 1 type of obstacles
    [SerializeField] private bool _onlySpawnDownObs = false;
    [SerializeField] private bool _onlySpawnMidObs = false;
    [SerializeField] private bool _onlySpawnUpObs = false;

    protected int _obstaclesSpawnedCount = 0;

    private PlayerController _playerController;

    private void Start() {
        _tf = gameObject.transform; 
        _nextSpawn = Mathf.RoundToInt(Time.time);
        SetUp();
    }
    
    private void SetUp(){
        _playerController = GameManager.Instance._playerController;
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

        obstacleIndex = GenerateSpawnIndex(); //Change index or set one for testing or tutorials
            
        if((obstacles.Length == 1 || _lastSpawnedIndex != obstacleIndex) && CanSpawnType(obstacleIndex)){
            ISpawnable spawnable = obstacles[obstacleIndex].GetComponent<ISpawnable>();
            spawnable.Spawn(_tf.position, _tf.rotation);
            Debug.Log(obstacles[obstacleIndex].transform.rotation);
            _obstaclesSpawnedCount++;
            _lastSpawnedIndex = obstacleIndex; //Update the lastSpawnedIndex
        }else{
            Spawn(); //Repeat if its the same index
        }
    }

    protected void SpawnSpecificObject(int objectIndex){
        obstacleIndex = objectIndex;
        Instantiate(obstacles[obstacleIndex], _tf.position, _tf.rotation);
    }

    private bool CanSpawnType(int obstacleIndex){
        ISpawnable.Type obsType = obstacles[obstacleIndex].GetComponent<Obstacle>().GetType();
        return !(_onlySpawnDownObs && obsType != ISpawnable.Type.DOWN
        || _onlySpawnMidObs && obsType != ISpawnable.Type.MID
        || _onlySpawnUpObs && obsType != ISpawnable.Type.UP);
    }
    
    protected int GenerateSpawnIndex(){

        Assert.IsFalse(obstacles.Length == 0, "The Spawn object has no obstacles assigned");
        if(obstacles.Length == 1)
            return 0;
        if(!_onlySpawnDownObs && _onlySpawnMidObs && !_onlySpawnUpObs) //Spawn Mid Obstacles
            return Random.Range(0 , 3);
        else if(_onlySpawnDownObs && !_onlySpawnMidObs && !_onlySpawnUpObs)//Spawn Down Obstacles
            return Random.Range(3 , 6);
        else if(!_onlySpawnDownObs && !_onlySpawnMidObs && _onlySpawnUpObs)//Spawn Up Obstacles
            return Random.Range(6 , 12);
        else
            return Random.Range(0 , obstacles.Length); //Spawn every type
    }

    public void SetSpawnType(bool spawnDown, bool spawnMid, bool spawnUp){ //For other classes, i.e tutorial
        _onlySpawnDownObs = spawnDown;
        _onlySpawnMidObs = spawnMid;
        _onlySpawnUpObs = spawnUp;
    }

    public void ResetCount(){
        _obstaclesSpawnedCount = 0;
    }
}
