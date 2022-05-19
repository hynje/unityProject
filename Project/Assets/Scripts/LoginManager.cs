using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Google;
using Facebook.Unity;
using Firebase.Auth;
using System.Threading.Tasks;

using UnityEngine.SceneManagement;

using Firebase;
using Firebase.Database;
using Firebase.Unity;

public class LoginManager : MonoBehaviour
{   
    // Auth �� instance
    FirebaseAuth auth = null;

    // ����� ����
    public static FirebaseUser user = null;

    // �����͸� ���� ���� reference ����
    DatabaseReference reference;

    // ��� ������ �Ǿ� �ִ� �������� üũ�Ѵ�.
    private bool signedIn = false;

    public static LoginManager instance;

    public static LoginManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                Debug.Log("Firebase.DependencyStatus.Available");
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                //app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.

                // �ʱ�ȭ
                auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
                reference = FirebaseDatabase.DefaultInstance.RootReference;

                // ������ �α��� ������ ��� �������� ����� ����ǰ� �̺�Ʈ�� �ɾ��ش�.
                auth.StateChanged += AuthStateChanged;
                //AuthStateChanged(this, null);
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });


        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback_Facebook, OnHideUnity);
        }
        else
        {
            FB.ActivateApp();
        }

    }


    // ���� �α��ο� ��� �������� �߻��� ����.
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            // ������ ������ ����� ������ ���ٸ� true�� �����Ѵ�. 
            signedIn = user != auth.CurrentUser && auth.CurrentUser != null;

            if (!signedIn && user != null)
            {
                UnityEngine.Debug.Log("Signed out " + user.UserId);
            }

            user = auth.CurrentUser;

            if (signedIn)
            {
                UnityEngine.Debug.Log("Signed in " + user.UserId);
            }
        }
    }

    private void InitCallback_Facebook()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else
        {
            Debug.LogError("Failed to Initialize the Facebook SDK");
        }
    }
    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }


    // ���� �α��� //
    public void GoogleLogout()
    {
        Debug.Log("Google Logout");
        GoogleSignIn.DefaultInstance.SignOut();
    }

    public void GoogleLogin()
    {
        if (GoogleSignIn.Configuration == null)
        {
            // ����
            GoogleSignIn.Configuration = new GoogleSignInConfiguration
            {
                RequestIdToken = true,
                RequestEmail = true,
                WebClientId = "856468702829-mke0pse0oe8b7bod29e2941osvhia83d.apps.googleusercontent.com"
            };
        }

        Task<GoogleSignInUser> signIn = GoogleSignIn.DefaultInstance.SignIn();

        TaskCompletionSource<FirebaseUser> signInCompleted = new TaskCompletionSource<FirebaseUser>();

        signIn.ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("Google Login task.IsCanceled");
            }
            else if (task.IsFaulted)
            {
                Debug.Log("Google Login task.IsFaulted");
            }
            else
            {
                Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(((Task<GoogleSignInUser>)task).Result.IdToken, null);
                auth.SignInWithCredentialAsync(credential).ContinueWith(authTask =>
                {
                    if (authTask.IsCanceled)
                    {
                        signInCompleted.SetCanceled();
                        Debug.Log("Google Login authTask.IsCanceled");
                        return;
                    }
                    if (authTask.IsFaulted)
                    {
                        signInCompleted.SetException(authTask.Exception);
                        Debug.Log("Google Login authTask.IsFaulted");
                        return;
                    }

                    user = authTask.Result;
                    Debug.LogFormat("Google User signed in successfully: {0} ({1})", user.DisplayName, user.UserId);

                    Firebase.Auth.FirebaseUser newUser = authTask.Result;

                    RealtimeDatabase.Instance.checkNewUser(newUser.UserId, newUser.DisplayName);
                    //RealtimeDatabase.Instance.checkTutorial(newUser.UserId);

                    return;
                });
            }
        });
    }

    // ���̽��� �α��� //
    public void FacebookLogout()
    {
        Debug.Log("Facebook Logout");
        FB.LogOut();
    }
    public void FacebookLogin()
    {
        List<string> perms = new List<string>() { "public_profile", "email", "user_friends" };
        FB.LogInWithReadPermissions(perms, FacebookAuthCallback);
    }
    private void FacebookAuthCallback(ILoginResult result)
    {
        if (result.Error != null)
        {
            Debug.LogError(string.Format("Facebook Login Error : {0}", result.Error));
        }
        else
        {
            if (FB.IsLoggedIn)
            {
                Debug.Log("Facebook Login!");

                AccessToken aToken = AccessToken.CurrentAccessToken;
                Debug.Log("ExpirationTime : " + aToken.ExpirationTime.ToString("MM/dd/yyyy HH:MM"));
                if (aToken.LastRefresh.HasValue)
                {
                    Debug.Log("LastRefresh : " + aToken.LastRefresh.Value.ToString("MM/dd/yyyy HH:MM"));
                }

                Debug.Log(string.Format("aToken string : {0}", aToken.TokenString));
                Debug.Log(string.Format("UserID : {0}", aToken.UserId));

                foreach (string perm in aToken.Permissions)
                {
                    Debug.Log(string.Format("permission : {0}", perm));
                }

                //FB.Mobile.RefreshCurrentAccessToken(Facebook_HandleResult);
                StartCoroutine(Co_FacebookLogin(aToken.TokenString));
            }
            else
            {
                Debug.LogError("User cancelled login");
            }
        }
    }
    private void Facebook_HandleResult(IResult result)
    {
        if (result == null)
        {
            Debug.LogError("Facebook_RefreshAccessToken Null Response");
            return;
        }
        if (!string.IsNullOrEmpty(result.Error))
        {
            Debug.LogError("Facebook_RefreshAccessToken Error Response : " + result.Error);
            return;
        }
        else if (result.Cancelled)
        {
            Debug.Log("Facebook_RefreshAccessToken Cancelled Response : " + result.RawResult);
            return;
        }
        else if (string.IsNullOrEmpty(result.RawResult))
        {
            Debug.LogError("Facebook_RefreshAccessToken Empty Response");
            return;
        }
        else
        {
            Debug.Log("Facebook_RefreshAccessToken Success Response :" + result.RawResult);
            StartCoroutine(Co_FacebookLogin(result.ResultDictionary["access_token"].ToString()));

            return;
        }
    }
    IEnumerator Co_FacebookLogin(string accessToken)
    {
        Debug.Log("Try to get Token..." + accessToken);
        while (System.String.IsNullOrEmpty(accessToken))
            yield return null;


        Credential credential = Firebase.Auth.FacebookAuthProvider.GetCredential(accessToken);
        Debug.Log("credential is " + credential.IsValid());
        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("Facebook Login authTask.IsCanceled");
                return;
            }
            if (task.IsFaulted)
            {
                if (task.Exception != null)
                {
                    Debug.LogError("Exception : " + task.Exception);
                }

                Debug.Log("Facebook Login authTask.IsFaulted");
                return;
            }

            user = task.Result;
            Debug.LogFormat("Facebook User signed in successfully: {0} ({1})", user.DisplayName, user.UserId);

            Firebase.Auth.FirebaseUser newUser = task.Result;
            RealtimeDatabase.Instance.checkNewUser(newUser.UserId, newUser.DisplayName);
            //RealtimeDatabase.Instance.checkTutorial(newUser.UserId);

            return;
        });
    }

    // ���� ����
    public void SignOut()
    {
        if (auth.CurrentUser != null)
            auth.SignOut();
    }

    // ���� ���� ����
    public void UserDelete()
    {
        if (auth.CurrentUser != null)
            auth.CurrentUser.DeleteAsync();
    }


}
