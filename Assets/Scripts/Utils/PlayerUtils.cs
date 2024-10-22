using System;

namespace Utils {
    public static class PlayerUtils {

        [Serializable]
        public enum HorizontalPos {
            Left,
            Middle,
            Right
        }

        public enum PlayerShape {
            Hexagon,
            Triangle,
            Circle
        }

        public enum VerticalPos {
            Up,
            Middle,
            Down
        }

        public const string ANIM_HPOS = "HPos";
        public const string ANIM_VPOS = "VPos";
    }

}