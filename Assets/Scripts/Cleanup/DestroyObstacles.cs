using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObstacles : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Obstacle" || other.tag == "Checkpoint"){
            Destroy(other.gameObject);     
            if(other.transform.parent != null) //avoid error if parent doesnt exist     
                Destroy(other.transform.parent.gameObject);
        }
    }
}
