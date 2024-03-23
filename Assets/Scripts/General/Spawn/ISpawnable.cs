using System;
using UnityEngine;

public interface ISpawnable
{
    public enum Type
    {
        Up,
        Mid,
        Down
    }

    void Spawn(Vector3 spawnObjPos, Quaternion spawnObjRot);
}
