using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameDirector : MonoBehaviour
{
    GameObject timer;
    GameObject pauseUi;
    float time;
    int minute, second;
    bool isPause;
    public float m_DoubleClickSecond = 0.25f; 
    private bool m_IsOneClick = false; 
    private double m_Timer = 0;

    void Start()
    {
        this.timer = GameObject.Find("Timer");
        this.pauseUi = GameObject.Find("Pause");
        isPause = false;
        pauseUi.SetActive(false);
    }

    void Update()
    {
        Pause();
        Timer();
    }

    void Pause()    //일시정지
    {
        if (m_IsOneClick && ((Time.time - m_Timer) > m_DoubleClickSecond)) { m_IsOneClick = false; }
        if (Input.GetMouseButtonDown(0))
        {
            if (!m_IsOneClick) { m_Timer = Time.time; m_IsOneClick = true; }
            else if (m_IsOneClick && ((Time.time - m_Timer) < m_DoubleClickSecond))
            {
                m_IsOneClick = false;
                if (isPause == false)
                {
                    Time.timeScale = 0;
                    isPause = true;
                    pauseUi.SetActive(true);
                    return;
                }
                if (isPause == true)
                {
                    Time.timeScale = 1;
                    isPause = false;
                    pauseUi.SetActive(false);
                    return;
                }
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
