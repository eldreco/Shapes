using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleA : Obstacle, ISpawnable
{
    public void Spawn(Vector3 spawnObjPos, Quaternion spawnObjRot)
    {
        Instantiate(this, spawnObjPos, spawnObjRot);
    }
}
