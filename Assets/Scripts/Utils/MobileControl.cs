using System;
using UnityEngine;

namespace Utils {
    public static class MobileControl {
        private static Vector2 _swipeStartPos;
        private static Vector2 _swipeEndPos;
        public static int SwipeDistance;

        public static event Action<Vector2, Vector2> OnSwipe;

        public static void SwipeCheck() {
            if (Input.touchCount <= 0) {
                return;
            }

            var touch = Input.GetTouch(0);

            switch (touch.phase) {
                case TouchPhase.Began:
                    _swipeStartPos = touch.position;
                    break;
                case TouchPhase.Ended:
                    _swipeEndPos = touch.position;
                    OnSwipe?.Invoke(_swipeStartPos, _swipeEndPos);
                    break;
            }
        }
    }
}