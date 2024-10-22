using GooglePlay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu {
    public class MenuUIController : MonoBehaviour {

        [SerializeField] private TMP_Text userNameText;
        [SerializeField] private Image profilePic;

        private void Awake() {
            GPServicesManager.SignIn();
            StartCoroutine(GPServicesManager.LoadProfilePic());
        }

        public void HandleAuthenticatedUI() {
            Debug.Log("Is auth:" + GPServicesManager.IsAuthenticated);

            if (GPServicesManager.IsAuthenticated) {
                ShowAuthenticatedUI();
            } else {
                ShowNotAuthenticatedUI();
            }
        }

        public void ShowAchievementsUI() {
            GPServicesManager.ShowAchievements();
        }

        public void ShowLeaderboardUI() {
            GPServicesManager.ShowLeaderboard();
        }

        private void ShowAuthenticatedUI() {
            profilePic.sprite = GPServicesManager.UserProfilePic;
            userNameText.text = GPServicesManager.UserName;
        }

        private void ShowNotAuthenticatedUI() {
            profilePic.gameObject.SetActive(false);
            userNameText.text = "Sign In to access social features, such as leaderboards and achievements.";
        }
    }
}