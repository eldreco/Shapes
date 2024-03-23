using System;

namespace PlayerUtils
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
            Down
        }
    }
    
}
