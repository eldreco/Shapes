using System;
using static Utils.PlayerUtils;

namespace Utils {

    [Serializable]
    public class PlayerState {

        public HorizontalPos hPos;
        public VerticalPos vPos;

        public PlayerState(HorizontalPos hPos, VerticalPos vPos) {
            this.hPos = hPos;
            this.vPos = vPos;
        }

        public static PlayerState GetDefaultClassicState() {
            return new PlayerState(HorizontalPos.Middle, VerticalPos.Middle);
        }

        protected bool Equals(PlayerState other) {
            return hPos == other.hPos && vPos == other.vPos;
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
            return HashCode.Combine((int)hPos, (int)vPos);
        }
    }
}