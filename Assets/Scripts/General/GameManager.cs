using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    public GameObject canvas;

    public bool levelEnded;

    public float obstacleVelocity;
    private float topVelocity;
    private float acceleration = 0.5f;

    public float timer;
    private float increaseInterval = 1.0f;


    private void Start() {
        SetBaseVelocity();
        timer = 0;
    }

    private void Update() {
        levelEnded = player.GetComponent<PlayerController>().GetLevelEnded();        
        if(Time.time >= timer && obstacleVelocity <= topVelocity){
            timer = (int)(Time.time + increaseInterval);
            obstacleVelocity *= 1.01f ;
        }
        if(obstacleVelocity > topVelocity)
            obstacleVelocity = topVelocity;
        if(levelEnded)
            obstacleVelocity = 0;
    }
    
    public void pauseGame(){
        canvas.GetComponent<CanvasController>().pauseGame();
        player.GetComponent<PlayerController>().setPause(true);
        Time.timeScale = 0;
        Debug.Log("Game Paused");
    }
    public void resumeGame(){
        canvas.GetComponent<CanvasController>().resumeGame();
        player.GetComponent<PlayerController>().setPause(false);
        Time.timeScale = 1;
    }


    public void ReloadCurrentLevel(){
        if(player.GetComponent<PlayerController>().GetLevelEnded() || canvas.GetComponent<CanvasController>().gameIsPaused()){
            Time.timeScale = 1; //Because of restarting when paused
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            canvas.GetComponent<CanvasController>().LevelStarted();
        }
    }

    public void OpenMainMenu(){
        Time.timeScale = 1; //Because it's clicked when menus are opened
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }

    public void OpenTutorial(){
        Time.timeScale = 1; //Because it's clicked when menus are opened
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
    }

    

    public void OpenClassic(){
        Time.timeScale = 1; //Because it's clicked when menus are opened
        SceneManager.LoadScene("Classic", LoadSceneMode.Single);
    }
    
    public GameObject GetPlayer(){
        return player;
    }

    public void SetBaseVelocity(){
        obstacleVelocity = 5f; //CHANGE IF NEEDED
        topVelocity = 15f;
    }

    public float GetVelocity(){
        return obstacleVelocity;
    }

    public void SetVelocity(float v){
        obstacleVelocity = v;
    }

    public void SetTopVelocity(float v){
        topVelocity = v;
    }

    public bool GetLevelEnded(){
        return levelEnded;
    }
}
