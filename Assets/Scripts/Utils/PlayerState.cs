using System;
using System.Collections.Generic;
using System.Linq;
using static Utils.PlayerUtils;

namespace Utils {

    [Serializable]
    public class PlayerState {

        public HorizontalPos HPos;
        public VerticalPos VPos;
        public PlayerShape Shape;

        public PlayerState(HorizontalPos hPos, VerticalPos vPos, PlayerShape shape) {
            HPos = hPos;
            VPos = vPos;
            Shape = shape;
        }

        public static PlayerState GetDefaultClassicState() {
            return new PlayerState(HorizontalPos.Middle, VerticalPos.Middle, PlayerShape.Square);
        }

        public static PlayerState GetDefaultShapesState() {
            return new PlayerState(HorizontalPos.Middle, VerticalPos.Middle, PlayerShape.Hexagon);
        }

        public static PlayerState[] GetAllStatesExcluding(PlayerState[] providedStates) {
            var allStates = (
                from hPos in (HorizontalPos[])Enum.GetValues(typeof(HorizontalPos))
                from vPos in (VerticalPos[])Enum.GetValues(typeof(VerticalPos))
                from shape in (PlayerShape[])Enum.GetValues(typeof(PlayerShape))
                select new PlayerState(hPos, vPos, shape)
            ).ToList();

            return allStates.Except(providedStates).ToArray();
        }
        
        public static PlayerState[] GetCollidingStatesForMultipleNonCollidingStatesObstacle(
            HorizontalPos hPos, 
            VerticalPos givenVPos, 
            PlayerShape givenShape
        ) {
            var allStates = (
                from vPos in (VerticalPos[])Enum.GetValues(typeof(VerticalPos))
                from shape in (PlayerShape[])Enum.GetValues(typeof(PlayerShape))
                where vPos != givenVPos || (vPos == givenVPos && shape != givenShape)
                select new PlayerState(hPos, vPos, shape)
            ).ToList();

            return allStates.ToArray();
        }

        public bool Equals(PlayerState other) {
            return HPos == other.HPos && VPos == other.VPos && Shape == other.Shape;
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
            return HashCode.Combine((int)HPos, (int)VPos, (int)Shape);
        }

    }
}