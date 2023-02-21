using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public float lifeTime = 1;

    private void Update() {
        lifeTime -= Time.deltaTime; //Set up the countdown

        if(lifeTime < 0){
            Destroy(gameObject);
        }
    }
}
