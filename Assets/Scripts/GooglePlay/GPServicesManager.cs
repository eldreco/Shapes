using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class GPServicesManager : MonoBehaviour
{
    public void Start() {
        SignIn();
    }

    public void SignIn(){
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status) {
        Debug.Log("ProcessAuthentication: " + status);
        if (status == SignInStatus.Success) {
            // Continue with Play Games Services
            Debug.Log("Sign in success");
        } else {
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
        }
    }
}