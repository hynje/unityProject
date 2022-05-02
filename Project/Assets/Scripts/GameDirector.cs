using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameDirector : MonoBehaviour
{
    GameObject timer;
    float time;
    int minute, second;
    bool isPause;
    void Start()
    {
        this.timer = GameObject.Find("Timer");
        isPause = false;
    }

    void Update()
    {
        Pause();
        Timer();
    }

    void Pause()    //일시정지 더블클릭으로 구현
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(isPause == false)
            {
                Time.timeScale = 0;
                isPause = true;
                return;
            }
            if(isPause == true)
            {
                Time.timeScale = 1;
                isPause=false;
                return;
            }
        }
    }

    void Timer()
    {
        if (!isPause)
        {
            time += Time.deltaTime;
            minute = (int)time / 60 % 60;
            second = (int)time % 60;
            this.timer.GetComponent<TextMeshProUGUI>().text = minute.ToString() + ":" + second.ToString("D2");
        }
    }
}
