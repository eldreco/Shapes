using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleT : Obstacle, ISpawnable
{
    public void Spawn(Vector3 spawnObjPos, Quaternion spawnObjRot)
    {
        Vector3 posT1 = new Vector3(spawnObjPos.x , spawnObjPos.y , -0.75f);
        Vector3 posT2 = new Vector3(spawnObjPos.x , spawnObjPos.y , 0.75f);
        int randIndex = Random.Range(0 , 3);

        if(randIndex <= 1){
            Instantiate(this, posT1,spawnObjRot);
            Instantiate(this, posT2,spawnObjRot);
        } else{
            int rndObsPos = Random.Range(0 , 2);
            if(rndObsPos == 0)
                Instantiate(this, posT1,spawnObjRot);
            else
                Instantiate(this, posT2,spawnObjRot);
        }
    }
}
