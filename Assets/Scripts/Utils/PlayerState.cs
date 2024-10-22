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
    }
}