using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using static Scripts.GooglePlay.GPGSIds;
using UnityEngine.Networking;

public class GPServicesManager : MonoBehaviour
{
    public static GPServicesManager Instance;

    public string UserName {get; private set;}
    public Sprite UserProfilePic {get; private set;}
    public bool IsAuthenticated {get; private set;}

    private void Awake(){
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OnEnable() {
        SignIn();
    }

    public void SignIn(){
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status) {
        Debug.Log("ProcessAuthentication: " + status);
        if (status == SignInStatus.Success) {
            AssignUserData();
            IsAuthenticated = true;
        } else {
            IsAuthenticated = false;
        }
    }

    public void UpdateLeaderboard(int score) {
        HandleAchievements(score);
        Social.ReportScore(score, leaderboard_highscore, (bool success) => {
            Debug.Log("UpdateLeaderboard: " + success);
        });
    }

    private void AssignUserData(){
        StartCoroutine(LoadImageFromUrl(PlayGamesPlatform.Instance.GetUserImageUrl()));
        UserName = PlayGamesPlatform.Instance.GetUserDisplayName();
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

    private IEnumerator LoadImageFromUrl (string url) {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if(request.result.Equals(UnityWebRequest.Result.ConnectionError) 
        || request.result.Equals(UnityWebRequest.Result.ProtocolError)) {
            Debug.LogError(request.error);
        } else {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            UserProfilePic = Sprite.Create(texture, new (0, 0, texture.width, texture.height), new(0.5f, 0.5f));
        }
    }
}
