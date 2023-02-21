using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public int _highScore{get; private set;}
    public bool _firstTime{get; private set;}

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();
    }

    public void SetHighScore(int hs){
        _highScore = hs;
        SaveHighScore();
    }

    [System.Serializable]
    public class SaveData
    {
        public bool firstTime;
        public int highScore;
    }

    public void SaveHighScore()
    {
        SaveData data = SaveRest();
        data.highScore = _highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            _highScore = data.highScore;
        }
    }

    private SaveData SaveRest(){
        SaveData d = new SaveData();
        d.firstTime = _firstTime;
        d.highScore = _highScore;
        return d;
    }
    
}


