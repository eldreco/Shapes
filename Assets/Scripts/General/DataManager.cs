using UnityEngine;
using System.IO;

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
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore(){
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
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
