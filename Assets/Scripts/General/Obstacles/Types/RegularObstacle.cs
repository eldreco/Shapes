using UnityEngine;

namespace General.Obstacles.Types
{
    public class RegularObstacle : Obstacle, ISpawnable
    {
        public void Spawn(Vector3 spawnObjPos, Quaternion spawnObjRot)
        {
            Instantiate(this, spawnObjPos, spawnObjRot);
        }
    }
}
