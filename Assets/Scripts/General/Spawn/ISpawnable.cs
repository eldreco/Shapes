using System;
using UnityEngine;

public interface ISpawnable
{
    void Spawn(Vector3 spawnObjPos, Quaternion spawnObjRot);
}
