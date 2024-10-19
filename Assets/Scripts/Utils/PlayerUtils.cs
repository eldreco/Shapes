using System;

namespace Utils
{
    public static class PlayerUtils
    {
        public const string ANIM_HPOS = "HPos";
        public const string ANIM_VPOS = "VPos";

        [Serializable]
        public enum HorizontalPos{
            Left, 
            Middle, 
            Right
        }

        public enum VerticalPos{
            Up,
            Middle,
            Down
        }
        
        public enum PlayerShape
        {
            Hexagon,
            Triangle,
            Circle
        }
    }
    
}
