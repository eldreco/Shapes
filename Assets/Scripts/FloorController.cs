using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    public GameObject floorLeft;
    public GameObject floorMiddle;
    public GameObject floorRight;

    private Animator fLeftAnim;
    private Animator fMiddleAnim;
    private Animator fRightAnim;

    public GameObject player;

    private void Start() {
        fLeftAnim = floorLeft.GetComponent<Animator>();
        fMiddleAnim = floorMiddle.GetComponent<Animator>();
        fRightAnim = floorRight.GetComponent<Animator>();
    }

    private void Update() {
        PlayerPosCheck();
    }

    private void PlayerPosCheck(){
        if(player.GetComponent<PlayerController>().getPos() == 1){
            fLeftAnim.SetBool("IsOn" , false);
            fMiddleAnim.SetBool("IsOn" , true);
            fRightAnim.SetBool("IsOn" , false);
        } else if(player.GetComponent<PlayerController>().getPos() == 0){
            fLeftAnim.SetBool("IsOn" , true);
            fMiddleAnim.SetBool("IsOn" , false);
            fRightAnim.SetBool("IsOn" , false);
        } else if(player.GetComponent<PlayerController>().getPos() == 2){
            fLeftAnim.SetBool("IsOn" , false);
            fMiddleAnim.SetBool("IsOn" , false);
            fRightAnim.SetBool("IsOn" , true);
        }
    }

}
