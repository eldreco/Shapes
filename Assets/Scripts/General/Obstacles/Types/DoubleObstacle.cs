using UnityEngine;
using Utils;
using static Utils.PlayerUtils;
using Random = UnityEngine.Random;

namespace General.Obstacles.Types {
    public class DoubleObstacle : Obstacle, ISpawnable {
        [SerializeField] private float offsetY;

        private new void Start() {
        }

        public void Spawn(Vector3 spawnObjPos, Quaternion spawnObjRot) {
            Vector3 leftPos = new(spawnObjPos.x, spawnObjPos.y + offsetY, -0.75f);
            Vector3 rightPos = new(spawnObjPos.x, spawnObjPos.y + offsetY, 0.75f);
            int randIndex = Random.Range(0, 3);

            if (randIndex <= 1) {
                SpawnSingleObstacle(spawnObjRot, leftPos);
                SpawnSingleObstacle(spawnObjRot, rightPos);
            } else {
                int rndObsPos = Random.Range(0, 2);
                SpawnSingleObstacle(spawnObjRot, rndObsPos == 0 ? leftPos : rightPos);
            }
        }

        private void SpawnSingleObstacle(Quaternion spawnObjRot, Vector3 pos) {
            NonCollidingStateList = SetNonCollidingStateList(HorizontalPos.Right);
            var newObject = Instantiate(this, pos, spawnObjRot);
            newObject.NonCollidingStateList = NonCollidingStateList;
        }

        private PlayerState[] SetNonCollidingStateList(HorizontalPos hPos) {
            return PlayerState.GetAllStatesExcluding(
                PlayerState.GetCollidingStatesForMultipleNonCollidingStatesObstacle(
                    hPos,
                    VerticalPos.Middle,
                    NonCollidingState.Shape
                )
            );
        }
    }
}