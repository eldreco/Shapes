using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleUL : Obstacle, ISpawnable
{
    public void Spawn(Vector3 spawnObjPos, Quaternion spawnObjRot)
    {
        Vector3 obstaclePos1 = new(spawnObjPos.x , 0f , -0.75f);
        Vector3 obstaclePos2 = new(spawnObjPos.x , 0f , 0.75f);
        int randIndex = Random.Range(0 , 3);

        if(randIndex <= 1){ //Spawn two obstacles
            Instantiate(this, obstaclePos1, spawnObjRot);
            Instantiate(this, obstaclePos2, spawnObjRot);
        } else if(randIndex == 2){//Spawn one obstacle
            int rndObsPos = Random.Range(0 , 2);
            if(rndObsPos == 0)
                Instantiate(this, obstaclePos1, spawnObjRot);//Spawn left
            else
                Instantiate(this, obstaclePos2, spawnObjRot);//Spawn right
        }
    }
}
