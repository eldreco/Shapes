using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.Networking;
using static GooglePlay.GPGSIds;

namespace GooglePlay {
    public static class GPServicesManager {
        public static string UserName { get; private set; }
        public static Sprite UserProfilePic { get; private set; }
        public static bool IsAuthenticated { get; private set; }

        public static void SignIn() {
            Debug.Log("Signing in");
            PlayGamesPlatform.Activate();
            PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
        }

        private static void ProcessAuthentication(SignInStatus status) {
            if (status == SignInStatus.Success) {
                UserName = PlayGamesPlatform.Instance.GetUserDisplayName();
                IsAuthenticated = true;
            } else {
                IsAuthenticated = false;
            }

            Debug.Log("ProcessAuthentication: " + status);
        }

        public static void UpdateLeaderboard(int score) {
            HandleAchievements(score);

            Social.ReportScore(
                score,
                leaderboard_highscore,
                success =>
                {
                    Debug.Log("UpdateLeaderboard: " + success);
                }
            );
        }

        private static void HandleAchievements(int score) {
            if (score >= 10) {
                UnlockAchievement(achievement_10_pointer);
            }
        }

        private static void UnlockAchievement(string achievementId) {
            Social.ReportProgress(
                achievementId,
                100.0f,
                success =>
                {
                    Debug.Log("UnlockAchievement: " + success);

                    if (success) {
                        Debug.Log("Achievement with ID: " + achievementId + " unlocked");
                    }
                }
            );
        }

        public static void ShowLeaderboard() {
            Social.ShowLeaderboardUI();
        }

        public static void ShowAchievements() {
            Social.ShowAchievementsUI();
        }

        public static IEnumerator LoadProfilePic() {
            Debug.Log("Loading profile pic");
            yield return new WaitUntil(() => PlayGamesPlatform.Instance.IsAuthenticated());
            string url = PlayGamesPlatform.Instance.GetUserImageUrl();
            var request = UnityWebRequestTexture.GetTexture(url);
            yield return request.SendWebRequest();

            if (request.result.Equals(UnityWebRequest.Result.ConnectionError)
                || request.result.Equals(UnityWebRequest.Result.ProtocolError)) {
                Debug.LogError(request.error);
            } else {
                var texture = ((DownloadHandlerTexture)request.downloadHandler).texture;

                UserProfilePic = Sprite.Create(
                    texture,
                    new Rect(
                        0,
                        0,
                        texture.width,
                        texture.height
                    ),
                    new Vector2(0.5f, 0.5f)
                );
                Debug.Log("Profile pic loaded");
            }
        }
    }
}