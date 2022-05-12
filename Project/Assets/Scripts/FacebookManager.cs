using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;           //페이스북 SDK를 사용하기 위한 네임스페이스?
using Firebase.Auth;            //파이어베이스 auth객체를 사용하기 위한 네임스페이스

public class FacebookManager : MonoBehaviour
{


    void Awake()
    {
        // IsInitialized : 초기화가 됐는지 확인하는 함수
        if (!FB.IsInitialized)
        {
            //초기화가 되지 않았다면 초기화를 진행(콜백,콜백)
            FB.Init(InitCallBack, OnHideUnity);
        }
        else
        {
            //초기화가 진행됐다면 개발자페이지의 앱을 활성화
            FB.ActivateApp();
        }


    }

    private void InitCallBack()
    {
        //초기화가 됐다면
        if (FB.IsInitialized)
            //개발자페이지의 앱활성화
            FB.ActivateApp();
        else
            Debug.Log("Failed Init");


    }
    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
            Time.timeScale = 0.0f;
        else
            Time.timeScale = 1.0f;

    }

    public void LoginButton()
    {
        //잘 모르겠다. 아마 권한인것같다.
        var Perms = new List<string>() { "public_profile" };

        //로그인 됐는지 확인하는 함수
        if (!FB.IsLoggedIn)
        {
            //로그인 안됐다면 로그인하는 함수(권한,콜백)
            FB.LogInWithReadPermissions(Perms, LoginCallback);
        }
        else
        {

        }

    }




    private void LoginCallback(ILoginResult result)
    {
        if (result.Cancelled)
        {
            Debug.Log("User cancelled login");
        }
        else
        {
            //토큰을 가져온다.
            var aToken = AccessToken.CurrentAccessToken;
            Debug.Log(aToken.UserId);
            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }

            //파이어베이스계정생성    
            StartCoroutine(coLogin(aToken));

        }
    }


    IEnumerator coLogin(AccessToken aToken)
    {

        //1.토큰가져온것 확인하기
        Debug.Log(string.Format("\n Try to get Token...{0}", aToken));

        //System.String.IsNullOrEmpty는 C#에서 사용할 수 있는 string클래스의 기능인듯
        while (System.String.IsNullOrEmpty(aToken.TokenString))
            yield return null;

        Debug.Log(string.Format("\n Try auth for Facebook...{0}", aToken.UserId));


        //파이어베이스인증객체생성
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;

        //credential은 인증서 같은 의미
        //FacebookAuthProvider : 페이스북에서 제공하는 액세스토큰을 가져오는것.
        Credential credential = FacebookAuthProvider.GetCredential(aToken.TokenString);

        //
        Debug.Log(string.Format("\n credential is {0}", credential));

        //credential을 통한 인증시도(여기가 실질적인 로그인)
        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("SignInWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log(string.Format("SignInWithCredentialAsync encounted an error : {0}", task.Exception));
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;

        });

    }

}