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

        protected bool Equals(ShapesPlayerState other) {
            return vPos == other.vPos && hPos == other.hPos && shape == other.shape;
        }

        public override bool Equals(object obj) {
            if (obj is null) {
                return false;
            }

            if (ReferenceEquals(this, obj)) {
                return true;
            }

            return obj.GetType() == GetType() && Equals((ShapesPlayerState)obj);
        }

        public override int GetHashCode() {
           return HashCode.Combine((int)hPos, (int)vPos, (int)shape);
        }
    }
}