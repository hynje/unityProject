using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoSceneManager : MonoBehaviour
{
    void Start()
    {
        Invoke("sceneChange", 3);
    }

    void sceneChange()
    {
        SceneManager.LoadScene("LoginScene");
    }
}
