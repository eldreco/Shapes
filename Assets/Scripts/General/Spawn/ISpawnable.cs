using System;
using UnityEngine;

public interface ISpawnable
{
    public enum Type
    {
        UP,
        MID,
        DOWN
    }

    void Spawn(Vector3 spawnObjPos, Quaternion spawnObjRot);
}
