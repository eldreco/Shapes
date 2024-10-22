using System;
using static Utils.PlayerUtils;

namespace Utils {

    [Serializable]
    public class ShapesPlayerState : PlayerState {

        public PlayerShape shape;

        public ShapesPlayerState(HorizontalPos hPos, VerticalPos vPos, PlayerShape shape) : base(hPos, vPos) {
            this.shape = shape;
        }

        public static ShapesPlayerState GetDefaultShapesState() {
            return new ShapesPlayerState(HorizontalPos.Middle, VerticalPos.Middle, PlayerShape.Hexagon);
        }
    }
}