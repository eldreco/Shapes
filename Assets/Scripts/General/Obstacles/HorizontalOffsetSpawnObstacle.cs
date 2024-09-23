using UnityEngine;

public class HorizontalOffsetSpawnObstacle : Obstacle, ISpawnable
{
    [SerializeField] private float offsetZ;

    public void Spawn(Vector3 spawnObjPos, Quaternion spawnObjRot)
    {
        Vector3 obstaclePos = new(spawnObjPos.x , spawnObjPos.y , offsetZ);
        Instantiate(this, obstaclePos, spawnObjRot);
    }
}
