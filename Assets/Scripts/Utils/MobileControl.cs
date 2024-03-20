using System;
using UnityEngine;

namespace MobileControl
{
    public static class MobileControl
    {
        private static Vector2 _swipeStartPos;
        private static Vector2 _swipeEndPos;
        public static int SwipeDistance;

        public static event Action<Vector2, Vector2> OnSwipe;

        public static void SwipeCheck(){
            if(Input.touchCount > 0){
                Touch touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Began)
                    _swipeStartPos = touch.position;
                else if(touch.phase == TouchPhase.Ended){
                    _swipeEndPos = touch.position;
                    OnSwipe?.Invoke(_swipeStartPos, _swipeEndPos);
                }
            }
        } 
    }
}