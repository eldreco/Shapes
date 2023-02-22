using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour {
    
    public void ActivateUI(GameObject UI){
        UI.SetActive(true);
    }

    public void ActivateUI(GameObject[] UI){
        foreach (GameObject gameObject in UI)
            gameObject.SetActive(true);
    }

    public void DeActivateUI(GameObject UI){
        UI.SetActive(false);
    }

    public void DeActivateUI(GameObject[] UI){
        foreach (GameObject gameObject in UI)
            gameObject.SetActive(false);
    }
}

