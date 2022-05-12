using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;           //���̽��� SDK�� ����ϱ� ���� ���ӽ����̽�?
using Firebase.Auth;            //���̾�̽� auth��ü�� ����ϱ� ���� ���ӽ����̽�

public class FacebookManager : MonoBehaviour
{


    void Awake()
    {
        // IsInitialized : �ʱ�ȭ�� �ƴ��� Ȯ���ϴ� �Լ�
        if (!FB.IsInitialized)
        {
            //�ʱ�ȭ�� ���� �ʾҴٸ� �ʱ�ȭ�� ����(�ݹ�,�ݹ�)
            FB.Init(InitCallBack, OnHideUnity);
        }
        else
        {
            //�ʱ�ȭ�� ����ƴٸ� �������������� ���� Ȱ��ȭ
            FB.ActivateApp();
        }


    }

    private void InitCallBack()
    {
        //�ʱ�ȭ�� �ƴٸ�
        if (FB.IsInitialized)
            //�������������� ��Ȱ��ȭ
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
        //�� �𸣰ڴ�. �Ƹ� �����ΰͰ���.
        var Perms = new List<string>() { "public_profile" };

        //�α��� �ƴ��� Ȯ���ϴ� �Լ�
        if (!FB.IsLoggedIn)
        {
            //�α��� �ȵƴٸ� �α����ϴ� �Լ�(����,�ݹ�)
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
            //��ū�� �����´�.
            var aToken = AccessToken.CurrentAccessToken;
            Debug.Log(aToken.UserId);
            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }

            //���̾�̽���������    
            StartCoroutine(coLogin(aToken));

        }
    }


    IEnumerator coLogin(AccessToken aToken)
    {

        //1.��ū�����°� Ȯ���ϱ�
        Debug.Log(string.Format("\n Try to get Token...{0}", aToken));

        //System.String.IsNullOrEmpty�� C#���� ����� �� �ִ� stringŬ������ ����ε�
        while (System.String.IsNullOrEmpty(aToken.TokenString))
            yield return null;

        Debug.Log(string.Format("\n Try auth for Facebook...{0}", aToken.UserId));


        //���̾�̽�������ü����
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;

        //credential�� ������ ���� �ǹ�
        //FacebookAuthProvider : ���̽��Ͽ��� �����ϴ� �׼�����ū�� �������°�.
        Credential credential = FacebookAuthProvider.GetCredential(aToken.TokenString);

        //
        Debug.Log(string.Format("\n credential is {0}", credential));

        //credential�� ���� �����õ�(���Ⱑ �������� �α���)
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