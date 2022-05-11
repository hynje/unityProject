using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Google;
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
    FirebaseUser user = null;

    // �����͸� ���� ���� reference ����
    DatabaseReference reference;

    // ��� ������ �Ǿ� �ִ� �������� üũ�Ѵ�.
    private bool signedIn = false;

    private void Awake()
    {
        // �ʱ�ȭ
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        // ������ �α��� ������ ��� �������� ����� ����ǰ� �̺�Ʈ�� �ɾ��ش�.
        auth.StateChanged += AuthStateChanged;
        //AuthStateChanged(this, null);
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

    // ���� �α��� //
    public void GoogleLoginProcessing()
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
                    writeNewUser(newUser.UserId, newUser.DisplayName);

                    SceneManager.LoadScene("MainScene");

                    return;
                });
            }
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

    public class User
    {
        public string username;
        public bool tutorial_state;
        public int highscore;
        public int score;

        public User() {
        }

        public User(string username, bool tutorial_state, int highscore, int score)
        {
            this.username = username;
            this.tutorial_state = false;
            this.highscore = highscore;
            this.score = score;
        }
    }

    void writeNewUser(string userId, string username)
    {
        User user = new User();
        string json = JsonUtility.ToJson(user);
        reference.Child(userId).SetRawJsonValueAsync(json);
        reference.Child(userId).Child("username").SetValueAsync(username);

    }

}
