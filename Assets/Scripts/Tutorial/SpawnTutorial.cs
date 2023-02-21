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
        tf = gameObject.transform; 
        nextSpawn = Mathf.RoundToInt(Time.time);
        _isActive = false;
        _spawnedCheckpoint = false;
    }

    private void Update() {
        SetInterval();
    }

    public new void SetInterval(){
        if(_isActive){
            if(!player.GetComponent<PlayerController>().GetLevelEnded()){
                interval = 2;
                TimerController();
            }
        }
    }

    private new void TimerController(){
        if (Time.time >= nextSpawn){
            nextSpawn = Mathf.RoundToInt(Time.time + interval);
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
