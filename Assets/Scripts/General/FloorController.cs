using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    private GameObject _player;

    private Renderer _middleRenderer;
    private Renderer _leftRenderer;
    private Renderer _rightRenderer;

    private void Start() {
        GameObject _floorLeft = gameObject.transform.Find("FloorLeft").gameObject;
        GameObject _floorMiddle = gameObject.transform.Find("FloorMiddle").gameObject;
        GameObject _floorRight = gameObject.transform.Find("FloorRight").gameObject;
        _middleRenderer = _floorMiddle.GetComponent<Renderer>();
        _leftRenderer = _floorLeft.GetComponent<Renderer>();
        _rightRenderer = _floorRight.GetComponent<Renderer>();
        _player = GameManager.Instance._player;
    }

    private void Update() {
        PlayerPosCheck();
    }

    private void PlayerPosCheck(){
        if(_player.GetComponent<PlayerController>().getPos() == 1){
            _leftRenderer.material.color = new Color(0.15f, 0.15f, 0.15f);
            _middleRenderer.material.color = Color.white;
            _rightRenderer.material.color = new Color(0.15f, 0.15f, 0.15f);
        } else if(_player.GetComponent<PlayerController>().getPos() == 0){
            _leftRenderer.material.color = Color.white;
            _middleRenderer.material.color = new Color(0.15f, 0.15f, 0.15f);
            _rightRenderer.material.color = new Color(0.15f, 0.15f, 0.15f);
        } else if(_player.GetComponent<PlayerController>().getPos() == 2){
            _leftRenderer.material.color = new Color(0.15f, 0.15f, 0.15f);
            _middleRenderer.material.color = new Color(0.15f, 0.15f, 0.15f);
            _rightRenderer.material.color = Color.white;
        }
    }

}
