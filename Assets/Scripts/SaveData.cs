using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SaveData
{
    private int highScore;
    private bool isFirstPlay;

    public void StartGameFirstTime(){
        if(Application.isEditor == false){
            if(PlayerPrefs.GetInt("FirstPlay", 1)==1){
                isFirstPlay = true;
                PlayerPrefs.SetInt ("FirstPlay", 0);
                PlayerPrefs.SetInt("HighScoreSaveData" , 0); //Save highscore
                PlayerPrefs.Save ();
            }else{
                isFirstPlay = false;
                highScore = PlayerPrefs.GetInt("HighScoreSaveData"); //Load highScore
            }  
        }
    }


    public int GetHighScore(){
        return highScore;
    }

    public void SetHighScore(int hs){
        highScore = hs;
        PlayerPrefs.SetInt("HighScoreSaveData" , hs);
    }

    public bool GetFirstPlay(){
        return isFirstPlay;
    }

    public void SetFirstPlay(bool firstPlay){
        isFirstPlay = firstPlay;
    }

}
