using UnityEngine;
using Utils;

namespace General.Obstacles.Types {
    
    [RequireComponent(typeof(Obstacle))]
    public class ShapesObstacle : MonoBehaviour {

        [SerializeField] private ShapesPlayerState[] nonCollidingStateList;

        public ShapesPlayerState[] NonCollidingStateList => nonCollidingStateList;
        
    }
}