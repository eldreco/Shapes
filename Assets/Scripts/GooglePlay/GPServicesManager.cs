using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using static Scripts.GooglePlay.GPGSIds;

public class GPServicesManager : MonoBehaviour
{
    public static GPServicesManager Instance;

    private void Awake(){
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Start() {
        SignIn();
    }

    public void SignIn(){
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status) {
        Debug.Log("ProcessAuthentication: " + status);
        if (status == SignInStatus.Success) {
            // Continue with Play Games Services
            Debug.Log("Sign in success");
        }
    }

    public void UpdateLeaderboard(int score) {
        HandleAchievements(score);
        Social.ReportScore(score, leaderboard_highscore, (bool success) => {
            Debug.Log("UpdateLeaderboard: " + success);
        });
    }

    private void HandleAchievements(int score){
        if(score >= 10){
            UnlockAchievement(achievement_10_pointer);
        }
    }

    private void UnlockAchievement(string achievementId) {
        Social.ReportProgress(achievementId, 100.0f, (bool success) => {
            Debug.Log("UnlockAchievement: " + success);
            if(success)
                Debug.Log("Achievement with ID: " + achievementId + " unlocked");
        });
    }

    public void ShowLeaderboard() {
        SignIn();
        Social.ShowLeaderboardUI();
    }

    public void ShowAchievements() {
        SignIn();
        Social.ShowAchievementsUI();
    }
}
