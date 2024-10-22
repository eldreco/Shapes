using General.Spawn;
using UnityEngine;
using Random = UnityEngine.Random;

namespace General.Obstacles.Types {
    public class DoubleObstacle : Obstacle, ISpawnable {
        [SerializeField] private float offsetY;

        public void Spawn(Vector3 spawnObjPos, Quaternion spawnObjRot) {
            Vector3 leftPos = new(spawnObjPos.x, spawnObjPos.y + offsetY, -0.75f);
            Vector3 rightPos = new(spawnObjPos.x, spawnObjPos.y + offsetY, 0.75f);
            int randIndex = Random.Range(0, 3);

            if (randIndex <= 1) {
                Instantiate(this, leftPos, spawnObjRot);
                Instantiate(this, rightPos, spawnObjRot);
            } else {
                int rndObsPos = Random.Range(0, 2);

                Instantiate(
                    this,
                    rndObsPos == 0 ? leftPos : rightPos,
                    spawnObjRot
                );
            }
        }
    }
}