using UnityEngine;
using Utils;

[DisallowMultipleComponent]
public class Obstacle : MonoBehaviour {

    [SerializeField] private bool singleNonCollidingState;
    [SerializeField] protected PlayerState nonCollidingState;

    public PlayerState NonCollidingState { get => nonCollidingState; set => nonCollidingState = value; }
    public PlayerState[] NonCollidingStateList { get; set; }

    protected void Start() {
        if (singleNonCollidingState) {
            NonCollidingStateList = new[] { NonCollidingState };
        } else {
            NonCollidingStateList = PlayerState.GetAllStatesExcluding(
                PlayerState.GetCollidingStatesForMultipleNonCollidingStatesObstacle(
                    nonCollidingState.HPos,
                    nonCollidingState.VPos,
                    nonCollidingState.Shape
                )
            );
        }
    }
}