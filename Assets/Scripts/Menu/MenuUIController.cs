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
    }

    public void HandleAuthenticatedUI(){
        if(GPServicesManager.Instance.IsAuthenticated)
            ShowAuthenticatedUI();
        else
            ShowNotAuthenticatedUI();
    }

    private void ShowAuthenticatedUI(){
        profilePic.sprite = GPServicesManager.Instance.UserProfilePic;
        userNameText.text = GPServicesManager.Instance.UserName;
    }

    private void ShowNotAuthenticatedUI(){
        profilePic.gameObject.SetActive(false);
        userNameText.text = "Sign In to access social features, such as leaderboards and achievements.";
    }
}
