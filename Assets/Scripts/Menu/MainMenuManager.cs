using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    public void startClassic(){
        Time.timeScale = 1; //Because it's clicked when menus are opened
        SceneManager.LoadScene("Classic", LoadSceneMode.Single);
    }

}
