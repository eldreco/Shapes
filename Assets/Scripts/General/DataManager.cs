using UnityEngine;
using System.IO;
using static Constants.Constants;
using UnityEngine.Assertions;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    private int classicHighScore;
    private int shapesHighScore;

    public bool IsFirstTimePlaying {get; private set;}

    private void Awake(){
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadData();
    }

    public void UpdateScore(GameMode mode, int hs){
        switch (mode){
            case GameMode.Classic:
                classicHighScore = hs;
                SaveClassicHighScore();
                break;
            case GameMode.Shapes:
                shapesHighScore = hs;
                SaveShapesHighScore();
                break;
        }
    }

    public int GetHighScoreForMode(GameMode mode){
        return mode switch
        {
            GameMode.Classic => classicHighScore,
            GameMode.Shapes => shapesHighScore,
            _ => -1,
        };
    }

    [System.Serializable]
    public class SaveData
    {
        public bool IsFirstTimePlayingData;
        public int ClassicHighScoreData;
        public int ShapesHighScoreData;
    }

    public void SaveClassicHighScore(){
        SaveData data = SaveRest();
        data.ClassicHighScoreData = classicHighScore;

        WriteData(data);
    }

    public void SaveShapesHighScore(){
        SaveData data = SaveRest();
        data.ShapesHighScoreData = shapesHighScore;

        WriteData(data);
    }

    public void LoadData(){
        Assert.IsTrue(File.Exists(SAVE_DATA_FILE_PATH), "Save data file not found");
        if (File.Exists(SAVE_DATA_FILE_PATH))
        {
            string json = File.ReadAllText(SAVE_DATA_FILE_PATH);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            classicHighScore = data.ClassicHighScoreData;
            IsFirstTimePlaying = data.IsFirstTimePlayingData;
            shapesHighScore = data.ShapesHighScoreData;
        }
    }

    private void WriteData(SaveData data){
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(SAVE_DATA_FILE_PATH, json);
    }

    private SaveData SaveRest(){
        SaveData data = new()
        {
            IsFirstTimePlayingData = IsFirstTimePlaying,
            ClassicHighScoreData = classicHighScore,
            ShapesHighScoreData = shapesHighScore
        };
        return data;
    }
    
}
