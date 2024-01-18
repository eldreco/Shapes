using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleUM : Obstacle, ISpawnable
{
    [SerializeField] private float _offsetY;

    public void Spawn(Vector3 spawnObjPos, Quaternion spawnObjRot)
    {
        Vector3 obstaclePos = new Vector3(spawnObjPos.x , _offsetY , spawnObjPos.z);
        Instantiate(this, obstaclePos, spawnObjRot);
    }
}
