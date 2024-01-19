using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidMid : Obstacle, ISpawnable
{
    private float _offset = 0.5f;

    public void Spawn(Vector3 spawnObjPos, Quaternion spawnObjRot)
    {
        Vector3 spawnPos = new(spawnObjPos.x, -_offset, -_offset);
        Quaternion spawnRot = Quaternion.Euler(90f, 0f, -90f);
        Instantiate(this, spawnPos, spawnRot);
    }
}
