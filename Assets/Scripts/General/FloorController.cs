using UnityEngine;
using static Utils.PlayerUtils;

namespace General {
    public class FloorController : MonoBehaviour {

        [SerializeField] private Renderer middleRenderer;
        [SerializeField] private Renderer leftRenderer;
        [SerializeField] private Renderer rightRenderer;

        private readonly Color _darkColor = new(0.15f, 0.15f, 0.15f);

        private void Start() {
            PlayerController.OnPlayerMovedH += PlayerPosCheck;
        }

        private void OnDisable() {
            PlayerController.OnPlayerMovedH -= PlayerPosCheck;
        }

        private void PlayerPosCheck(HorizontalPos pos) {
            leftRenderer.material.color = pos == HorizontalPos.Left ? Color.white : _darkColor;
            middleRenderer.material.color = pos == HorizontalPos.Middle ? Color.white : _darkColor;
            rightRenderer.material.color = pos == HorizontalPos.Right ? Color.white : _darkColor;
        }
    }
}