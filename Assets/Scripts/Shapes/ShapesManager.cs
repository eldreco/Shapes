using System.Collections.Generic;
using General;
using UnityEngine;
using Utils;
using static Utils.PlayerUtils;

namespace Shapes {

    [DisallowMultipleComponent]
    public class ShapesManager : MonoBehaviour {
        public static ShapesManager Instance;

        [SerializeField] private Mesh hexagonMesh;
        [SerializeField] private Mesh pyramidMesh;
        [SerializeField] private Mesh cylinderMesh;

        public Dictionary<PlayerShape, Mesh> ShapeMeshMap { get; } = new();

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
            }

            Instance = this;
        }

        private void Start() {
            ShapeMeshMap.Add(PlayerShape.Hexagon, hexagonMesh);
            ShapeMeshMap.Add(PlayerShape.Triangle, pyramidMesh);
            ShapeMeshMap.Add(PlayerShape.Circle, cylinderMesh);
            GameManager.Instance.ActiveGameMode = GameMode.Shapes;
        }

        public void PressCircle() {
            ShapesPlayerController.Instance.ChangeShape(PlayerShape.Circle);
        }

        public void PressHexagon() {
            ShapesPlayerController.Instance.ChangeShape(PlayerShape.Hexagon);
        }

        public void PressTriangle() {
            ShapesPlayerController.Instance.ChangeShape(PlayerShape.Triangle);
        }
    }
}