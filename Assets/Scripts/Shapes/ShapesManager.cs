using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapesManager : GameManager
{
    // public static new ShapesManager Instance;

    public void PressCircle(){
        ShapesPlayerController.Instance.ChangeToShpere();
    }

    public void PressSquare(){
        ShapesPlayerController.Instance.ChangeToCube();
    }

    public void PressTriangle(){
        ShapesPlayerController.Instance.ChangeToPyramid();
    }
}
