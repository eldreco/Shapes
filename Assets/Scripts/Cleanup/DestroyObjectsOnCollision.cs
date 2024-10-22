using UnityEngine;

namespace Cleanup {
    public class DestroyObjectsOnCollision : MonoBehaviour {

        private void OnTriggerEnter(Collider other) {
            Destroy(other.gameObject);
        }
    }
}