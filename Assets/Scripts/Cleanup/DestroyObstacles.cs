using System.Collections.Generic;
using UnityEngine;

public class DestroyObstacles : MonoBehaviour
{
    private readonly List<string> tagsToDestroy = new() 
    { 
        "Obstacle",
        "Checkpoint" 
    };
    
    private void OnTriggerEnter(Collider other) {
        if(tagsToDestroy.Contains(other.tag)){
            Destroy(other.gameObject);     
            if(other.transform.parent != null)
                Destroy(other.transform.parent.gameObject);
        }
    }
}
