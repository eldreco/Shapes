using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTutorial : SpawnObstacles
{
    public static SpawnTutorial Instance;

    public bool _isActive {get; set;}
    private bool _spawnedCheckpoint;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    private void Start() {
        _tf = gameObject.transform; 
        _nextSpawn = Mathf.RoundToInt(Time.time);
        _isActive = false;
        _spawnedCheckpoint = false;
    }

    private void Update() {
        SetInterval();
    }

    public new void SetInterval(){
        if(_isActive){
            if(!PlayerController.Instance._levelEnded){
                _interval = 2;
                TimerController();
            }
        }
    }

    private new void TimerController(){
        if (Time.time >= _nextSpawn){
            _nextSpawn = Mathf.RoundToInt(Time.time + _interval);
            if(_obstaclesSpawnedCount < 10)
                Spawn();
            else if (_obstaclesSpawnedCount == 10 && !_spawnedCheckpoint){
                StartCoroutine(SpawnStage());
                _spawnedCheckpoint = true;
            }
        }
    }

    public IEnumerator SpawnStage(){
        yield return new WaitForSeconds(1);
        SpawnSpecificObject(11);
        this._isActive = false;
    }

    public void ResetCheckpoint(){
        _spawnedCheckpoint = false;
    }
}
