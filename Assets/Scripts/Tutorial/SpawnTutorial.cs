using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTutorial : SpawnObstacles
{
    public bool isActive;

    private void Update() {
        SetInterval();
    }

    private void Start() {
        tf = gameObject.transform; 
        nextSpawn = Mathf.RoundToInt(Time.time);
        isActive = false;
        // StartCoroutine(FirstStage());
    }

    public new void SetInterval(){
        if(isActive){
            if(!player.GetComponent<PlayerController>().GetLevelEnded()){
                interval = 2;
                TimerController();
            }
        }
    }

    public void setActive(bool active){
        isActive = active;
    }

    private new void TimerController(){
        if (Time.time >= nextSpawn){
            nextSpawn = Mathf.RoundToInt(Time.time + interval);
            Spawn();
        }
    }

    public IEnumerator SpawnStage(){
        yield return new WaitForSeconds(20);
        SpawnSpecificObject(11);
        setActive(false);
    }
}
