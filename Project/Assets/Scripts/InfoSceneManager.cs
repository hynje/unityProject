using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoSceneManager : MonoBehaviour
{
    AudioSource aud;

    void Start()
    {
        aud = GetComponent<AudioSource>();
        Invoke("sceneChange", 9);
    }

    void sceneChange()
    {
        SceneManager.LoadScene("LoginScene");
    }
}
