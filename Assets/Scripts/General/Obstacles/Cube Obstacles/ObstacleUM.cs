using UnityEngine;

public class ObstacleUM : Obstacle, ISpawnable
{
    [SerializeField] private float offsetY;

    public void Spawn(Vector3 spawnObjPos, Quaternion spawnObjRot)
    {
        Vector3 obstaclePos = new(spawnObjPos.x , offsetY , spawnObjPos.z);
        Instantiate(this, obstaclePos, spawnObjRot);
    }
}
