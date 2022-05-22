using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    GameObject ScoreUI;
    TextMeshProUGUI score;
    public float time;
    int minute, second;
    void Start()
    {
        ScoreUI = GameObject.Find("ScoreCanvas");
        score = GetComponent<TextMeshProUGUI>();
        score.enabled = false;
        DontDestroyOnLoad(ScoreUI);
        
    }

    void Update()
    {
        SaveScore();
    }

    void SaveScore()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            if (Time.timeScale != 0)
            {
                time += Time.deltaTime;
                minute = (int)time / 60 % 60;
                second = (int)time % 60;
                score.text = minute.ToString() + ":" + second.ToString("D2");
            }
        }
        else
            score.enabled = true;
        
    }
}
