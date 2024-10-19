using System;
using static Utils.PlayerUtils;

namespace Utils {

    [Serializable]
    public class PlayerState {

        public HorizontalPos HPos;
        public VerticalPos VPos;

        public PlayerState(HorizontalPos hPos, VerticalPos vPos) {
            HPos = hPos;
            VPos = vPos;
        }

        public static PlayerState GetDefaultClassicState() {
            return new PlayerState(HorizontalPos.Middle, VerticalPos.Middle);
        }

        protected bool Equals(PlayerState other) {
            return HPos == other.HPos && VPos == other.VPos;
        }

        public override bool Equals(object obj) {
            if (obj is null) {
                return false;
            }

            if (ReferenceEquals(this, obj)) {
                return true;
            }

            return obj.GetType() == GetType() && Equals((PlayerState)obj);
        }

        public override int GetHashCode() {
            return HashCode.Combine((int)HPos, (int)VPos);
        }
    }
}