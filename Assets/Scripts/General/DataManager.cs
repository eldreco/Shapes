using System;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;
using Utils;
using static Utils.Constants;

namespace General {
    public class DataManager : MonoBehaviour {
        public static DataManager Instance;

        private HighScoresDictionary _highScores = new()
        {
            { GameMode.Classic, 0 },
            { GameMode.Shapes, 0 }
        };

        private bool IsFirstTimePlaying { get; set; }

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }

        public void UpdateHighScore(GameMode gameMode, int newHighScore) {
            _highScores[gameMode] = newHighScore;
            SaveHighScores();
        }

        public int GetHighScore(GameMode mode) {
            return _highScores[mode];
        }

        public void LoadData() {
            Assert.IsTrue(File.Exists(SaveDataFilePath), "Save data file not found");

            if (File.Exists(SaveDataFilePath)) {
                string json = File.ReadAllText(SaveDataFilePath);
                var data = JsonUtility.FromJson<SaveData>(json);

                if (data.highScoreData == null || data.highScoreData.Count == 0) {
                    data.highScoreData = new HighScoresDictionary
                    {
                        { GameMode.Classic, 0 },
                        { GameMode.Shapes, 0 }
                    };
                }
                IsFirstTimePlaying = data.isFirstTimePlayingData;
                _highScores = data.highScoreData;
            }
        }

        private void SaveHighScores() {
            var data = SaveRest();
            data.highScoreData = _highScores;

            WriteData(data);
        }

        private void WriteData(SaveData data) {
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(SaveDataFilePath, json);
        }

        private SaveData SaveRest() {
            SaveData data = new()
            {
                isFirstTimePlayingData = IsFirstTimePlaying,
                highScoreData = _highScores
            };
            return data;
        }

        [Serializable]
        public class SaveData {
            public bool isFirstTimePlayingData;
            [NotNull] public HighScoresDictionary highScoreData;
        }
    }
}