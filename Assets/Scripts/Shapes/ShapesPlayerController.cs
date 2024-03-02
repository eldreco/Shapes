using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class ShapesPlayerController : PlayerController
{
    public static new ShapesPlayerController Instance;

    private enum Shape{SPHERE, CUBE, PYRAMID};

    [SerializeField] private Shape _activeShape;

    private void Awake() {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    private void Start() {
        Setup();
        _activeShape = Shape.CUBE;
    }

    public void ChangeToShpere(){
        if(_activeShape != Shape.SPHERE && _hPos == 1){
            _anim.SetTrigger("SphereM");
            _activeShape = Shape.SPHERE; 
        }
    }

    public void ChangeToCube(){
        if(_activeShape != Shape.CUBE && _hPos == 1){
            _anim.SetTrigger("CubeM");
            _activeShape = Shape.CUBE; 
        }
    }

    public void ChangeToPyramid(){
        // if(_activeShape != Shape.PYRAMID && _pos == 1){
        //     _anim.SetTrigger("PyramidM");
        //     _activeShape = Shape.PYRAMID; 
        // }
        _anim.Play("PPM");
    }
}
