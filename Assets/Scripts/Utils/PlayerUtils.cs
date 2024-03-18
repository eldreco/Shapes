using System;

namespace PlayerUtils
{
    public static class PlayerUtils
    {
        public const string ANIM_HPOS = "HPos";
        public const string ANIM_VPOS = "VPos";

        [Serializable]
        public enum HorizontalPos{
            LEFT, 
            MIDDLE, 
            RIGHT
        }

        public enum VerticalPos{
            UP,
            DOWN
        }
    }
    
}
