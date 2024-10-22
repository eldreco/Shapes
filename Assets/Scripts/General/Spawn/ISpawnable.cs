using UnityEngine;

namespace General.Spawn {
    public interface ISpawnable {
        void Spawn(Vector3 spawnObjPos, Quaternion spawnObjRot);
    }
}