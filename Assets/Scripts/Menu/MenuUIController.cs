using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuUIController : MonoBehaviour
{
    public static MenuUIController Instance;

    [SerializeField] private TMP_Text userNameText;
    [SerializeField] private Image profilePic;

    private void Awake(){
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        GPServicesManager.SignIn();
        StartCoroutine(GPServicesManager.LoadProfilePic());
    }

    public void HandleAuthenticatedUI(){
        Debug.Log("Is auth:" + GPServicesManager.IsAuthenticated);
        if(GPServicesManager.IsAuthenticated)
            ShowAuthenticatedUI();
        else
            ShowNotAuthenticatedUI();
    }

    public void ShowAchievementsUI(){
        GPServicesManager.ShowAchievements();
    }

    public void ShowLeaderboardUI(){
        GPServicesManager.ShowLeaderboard();
    }

    private void ShowAuthenticatedUI(){
        profilePic.sprite = GPServicesManager.UserProfilePic;
        userNameText.text = GPServicesManager.UserName;
    }

    private void ShowNotAuthenticatedUI(){
        profilePic.gameObject.SetActive(false);
        userNameText.text = "Sign In to access social features, such as leaderboards and achievements.";
    }
}
