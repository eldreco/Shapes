using UnityEngine;

namespace General.Obstacles.Types
{
    public class SpawnOffsetObstacle : Obstacle, ISpawnable
    {
        [SerializeField] private float verticalOffset;
        [SerializeField] private float horizontalOffset;

        public void Spawn(Vector3 spawnObjPos, Quaternion spawnObjRot)
        {
            Vector3 obstaclePos = new(
                spawnObjPos.x ,
                spawnObjPos.y + verticalOffset , 
                spawnObjPos.z + horizontalOffset
            );
            
            Instantiate(this, obstaclePos, spawnObjRot);
        }
    }
}