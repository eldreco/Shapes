using UnityEngine;

namespace General.Obstacles {
    public class ObstacleController : MonoBehaviour {
        private Rigidbody _rb;

        protected void Start() {
            _rb = GetComponent<Rigidbody>();
        }

        protected void FixedUpdate() {
            MoveObstacle(GameManager.Instance.ObstacleVelocity);
        }

        private void MoveObstacle(float speed) {
            _rb.MovePosition(transform.position + speed * Time.deltaTime * transform.right);
        }
    }
}