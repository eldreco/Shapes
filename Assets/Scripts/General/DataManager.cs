using UnityEngine;
using System.IO;
using static Constants.Constants;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public int HighScore {get; private set;}
    public bool IsFirstTimePlaying {get; private set;}

    private void Awake(){
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();
    }

    public void SetHighScore(int hs){
        HighScore = hs;
        SaveHighScore();
    }

    [System.Serializable]
    public class SaveData
    {
        public bool IsFirstTimePlayingData;
        public int HighScoreData;
    }

    public void SaveHighScore(){
        SaveData data = SaveRest();
        data.HighScoreData = HighScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(SAVE_DATA_FILE_PATH, json);
    }

    public void LoadHighScore(){
        if (File.Exists(SAVE_DATA_FILE_PATH))
        {
            string json = File.ReadAllText(SAVE_DATA_FILE_PATH);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            HighScore = data.HighScoreData;
        }
    }

    private SaveData SaveRest(){
        SaveData data = new()
        {
            IsFirstTimePlayingData = IsFirstTimePlaying,
            HighScoreData = HighScore
        };
        return data;
    }
    
}
