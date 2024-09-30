using UnityEngine;

public class DoubleOffsetSpawnObstacle : Obstacle, ISpawnable
{
    [SerializeField] private float offsetY;
    [SerializeField] private float offsetZ;

    public void Spawn(Vector3 spawnObjPos, Quaternion spawnObjRot)
    {
        Vector3 obstaclePos = new(spawnObjPos.x , offsetY , offsetZ);
        Instantiate(this, obstaclePos, spawnObjRot);
    }
}
