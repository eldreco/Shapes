using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapesPlayerController : PlayerController
{
    public static new ShapesPlayerController Instance;

    private enum Shape{SPHERE, CUBE, PYRAMID};

    private Shape _activeShape;

    private void Awake() {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    private void Start() {
        Setup();
        _activeShape = Shape.CUBE;
        // ChangeToCube();
    }

    public void ChangeToShpere(){
        if(_activeShape != Shape.SPHERE){
            _anim.SetTrigger("SphereM");
            _activeShape = Shape.SPHERE; 
        }
    }

    public void ChangeToCube(){
        if(_activeShape != Shape.CUBE){
            _anim.SetTrigger("CubeM");
            _activeShape = Shape.CUBE; 
        }
    }

    public void ChangeToPyramid(){
        if(_activeShape != Shape.PYRAMID){
            _activeShape = Shape.PYRAMID; 
        }
    }
}
