using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    public GameObject floorLeft;
    public GameObject floorMiddle;
    public GameObject floorRight;

    public GameObject player;

    private Renderer middleRenderer;
    private Renderer leftRenderer;
    private Renderer rightRenderer;

    private void Start() {
        middleRenderer = floorMiddle.GetComponent<Renderer>();
        leftRenderer = floorLeft.GetComponent<Renderer>();
        rightRenderer = floorRight.GetComponent<Renderer>();
    }

    private void Update() {
        PlayerPosCheck();
    }

    private void PlayerPosCheck(){
        if(player.GetComponent<PlayerController>().getPos() == 1){
            leftRenderer.material.color = new Color(0.15f, 0.15f, 0.15f);
            middleRenderer.material.color = Color.white;
            rightRenderer.material.color = new Color(0.15f, 0.15f, 0.15f);
        } else if(player.GetComponent<PlayerController>().getPos() == 0){
            leftRenderer.material.color = Color.white;
            middleRenderer.material.color = new Color(0.15f, 0.15f, 0.15f);
            rightRenderer.material.color = new Color(0.15f, 0.15f, 0.15f);
        } else if(player.GetComponent<PlayerController>().getPos() == 2){
            leftRenderer.material.color = new Color(0.15f, 0.15f, 0.15f);
            middleRenderer.material.color = new Color(0.15f, 0.15f, 0.15f);
            rightRenderer.material.color = Color.white;
        }
    }

}
