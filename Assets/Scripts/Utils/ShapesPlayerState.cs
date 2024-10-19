using System;
using static Utils.PlayerUtils;

namespace Utils {
    
    [Serializable]
    public class ShapesPlayerState : PlayerState {

        public PlayerShape Shape;

        public ShapesPlayerState(HorizontalPos hPos, VerticalPos vPos, PlayerShape shape) : base(hPos, vPos) {
            Shape = shape;
        }

        public static ShapesPlayerState GetDefaultShapesState() {
            return new ShapesPlayerState(HorizontalPos.Middle, VerticalPos.Middle, PlayerShape.Hexagon);
        }
    }
}