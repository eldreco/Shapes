using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTutorial : SpawnObstacles
{
    public bool _isActive;
    private bool _spawnedCheckpoint;

    private void Start() {
        tf = gameObject.transform; 
        nextSpawn = Mathf.RoundToInt(Time.time);
        _isActive = false;
        _spawnedCheckpoint = false;
        // StartCoroutine(FirstStage());
    }

    private void Update() {
        SetInterval();
        Debug.Log(_obstaclesSpawnedCount);
    }

    public new void SetInterval(){
        if(_isActive){
            if(!player.GetComponent<PlayerController>().GetLevelEnded()){
                interval = 2;
                TimerController();
            }
        }
    }

    public void setActive(bool active){
        _isActive = active;
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
        setActive(false);
    }

    public void ResetCheckpoint(){
        _spawnedCheckpoint = false;
    }
}
