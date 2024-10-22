using UnityEngine;
using UnityEngine.Assertions;

namespace Cleanup {
    public class DestroyOverTime : MonoBehaviour {
        [SerializeField] private float lifeTime;

        private void Start() {
            Assert.IsTrue(lifeTime > 0);
        }

        private void Update() {
            lifeTime -= Time.deltaTime;

            if (lifeTime < 0) {
                Destroy(gameObject);
            }
        }
    }
}