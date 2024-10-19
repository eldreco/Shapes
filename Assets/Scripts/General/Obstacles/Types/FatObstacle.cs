using UnityEngine;

namespace General.Obstacles.Types
{
    public class FatObstacle : Obstacle, ISpawnable
    {
        private const float offsetZ = 0.25f;

        public void Spawn(Vector3 spawnObjPos, Quaternion spawnObjRot) {
            Vector3 leftPos = new(spawnObjPos.x, spawnObjPos.y, -offsetZ);
            Vector3 rightPos = new(spawnObjPos.x, spawnObjPos.y, offsetZ);
            int rndLPos = Random.Range(0, 2);

            if (rndLPos == 0) {
                Instantiate(this, leftPos, spawnObjRot);
            } else {
                Instantiate(this, rightPos, spawnObjRot);
            }
        }
    }
}