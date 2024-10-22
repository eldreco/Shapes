using General.Spawn;
using UnityEngine;

namespace General.Obstacles.Types {
    public class FatObstacle : Obstacle, ISpawnable {
        private const float OffsetZ = 0.25f;

        public void Spawn(Vector3 spawnObjPos, Quaternion spawnObjRot) {
            Vector3 leftPos = new(spawnObjPos.x, spawnObjPos.y, -OffsetZ);
            Vector3 rightPos = new(spawnObjPos.x, spawnObjPos.y, OffsetZ);
            int rndLPos = Random.Range(0, 2);

            Instantiate(this, rndLPos == 0 ? leftPos : rightPos, spawnObjRot);
        }
    }
}