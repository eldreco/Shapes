using System;
using System.Collections.Generic;
using GooglePlay;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using static Utils.MobileControl;
using static Utils.Constants;

namespace General {

    [DisallowMultipleComponent]
    public class GameManager : MonoBehaviour {
        public static GameManager Instance;

        public readonly Dictionary<GameMode, int> CurrentScoreMap = new()
        {
            { GameMode.Classic, 0 },
            { GameMode.Shapes, 0 }
        };

        private Timer _increaseVelocityTimer;
        private float _obstacleVelocity = 5f;
        
        public bool IsLevelEnded { get; private set; }
        public bool IsGamePaused { get; private set; }

        public float ObstacleVelocity
        {
            get => _obstacleVelocity;
            set => _obstacleVelocity = value <= TOP_OBSTACLE_VELOCITY ? value : _obstacleVelocity;
        }

        public GameMode ActiveGameMode { get; set; } = GameMode.Classic;

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
            }

            Instance = this;
        }

        private void Start() {
            DataManager.Instance.LoadData();

            SwipeDistance = SceneManager.GetActiveScene().name.Equals(MAIN_MENU_SCENE)
                ? 25
                : 5;
        }

        private void Update() {
            _increaseVelocityTimer.ExecuteTimer();
        }

        private void OnEnable() {
            _increaseVelocityTimer = new Timer(1f);
            _increaseVelocityTimer.OnTimerFinished += IncreaseVelocity;
            PlayerController.OnObstaclePassed += UpdateScore;
            SetBaseVelocity();
        }

        private void OnDisable() {
            _increaseVelocityTimer.OnTimerFinished -= IncreaseVelocity;
            PlayerController.OnObstaclePassed -= UpdateScore;
        }

        public event Action OnLevelEnded;

        private void UpdateScore() {
            CurrentScoreMap[ActiveGameMode]++;
            int score = CurrentScoreMap[ActiveGameMode];
            int highScore = DataManager.Instance.GetHighScore(ActiveGameMode);

            if (score <= highScore) {
                return;
            }

            highScore = score;
            DataManager.Instance.UpdateHighScore(ActiveGameMode, highScore);
            GPServicesManager.UpdateLeaderboard(highScore);
        }

        private void IncreaseVelocity() {
            ObstacleVelocity *= OBSTACLE_ACCELERATION;
            _increaseVelocityTimer.ResetTimer();
        }

        public void PauseGame() {
            IsGamePaused = true;
            Time.timeScale = 0;
        }

        public void ResumeGame() {
            IsGamePaused = false;
            Time.timeScale = 1;
        }

        public void ReloadCurrentLevel() {
            if (!IsLevelEnded && !IsGamePaused) {
                return;
            }

            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void LevelEnded() {
            IsLevelEnded = true;
            ObstacleVelocity = 0;
            OnLevelEnded?.Invoke();
        }

        public void OpenMainMenu() {
            Time.timeScale = 1;
            SceneManager.LoadScene(MAIN_MENU_SCENE, LoadSceneMode.Single);
        }

        public void OpenTutorial() {
            Time.timeScale = 1;
            SceneManager.LoadScene(TUTORIAL_SCENE, LoadSceneMode.Single);
        }

        public void OpenClassic() {
            Time.timeScale = 1;
            SceneManager.LoadScene(CLASSIC_SCENE, LoadSceneMode.Single);
        }

        public void OpenShapes() {
            Time.timeScale = 1;
            SceneManager.LoadScene(SHAPES_SCENE, LoadSceneMode.Single);
        }

        private void SetBaseVelocity() {
            ObstacleVelocity = OBSTACLE_VELOCITY;
        }
    }
}