using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameDirector : MonoBehaviour
{
    GameObject timer;
    GameObject pauseUi;
    public AudioClip to_main;
    AudioSource aud;

    float time;
    int minute, second;

    bool isPause;
    public float m_DoubleClickSecond = 0.25f; 
    private bool m_IsOneClick = false; 
    private double m_Timer = 0;

    private Vector2 initialPos;
    private bool exitcnt = false;
    void Start()
    {
        this.timer = GameObject.Find("Timer");
        this.pauseUi = GameObject.Find("Pause");
        aud = GetComponent<AudioSource>();
        isPause = false;
        pauseUi.SetActive(false);
    }

    void Update()
    {
        Pause();
        Timer();
        if (isPause)
        {
            if (Input.GetMouseButtonDown(0))
            {
                initialPos = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                Swipe(Input.mousePosition);
            }
        }
    }

    void Pause()    //일시정지
    {
        if (m_IsOneClick && ((Time.realtimeSinceStartup - m_Timer) > m_DoubleClickSecond))
        {
            m_IsOneClick = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (!m_IsOneClick) {
                m_Timer = Time.realtimeSinceStartup;
                Debug.Log(m_Timer);
                m_IsOneClick = true;
            }
            else if (m_IsOneClick && ((Time.realtimeSinceStartup - m_Timer) < m_DoubleClickSecond))
            {
                m_IsOneClick = false;
                if (isPause == false)
                {
                    Time.timeScale = 0;
                    isPause = true;
                    pauseUi.SetActive(true);
                    return;
                }
                else
                {
                    pauseUi.SetActive(false);
                    isPause = false;
                    exitcnt = false;
                    Time.timeScale = 1;
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

    void Swipe(Vector3 finalPos)
    {
        float disX = Mathf.Abs(initialPos.x - finalPos.x);
        float disY = Mathf.Abs(initialPos.y - finalPos.y);
        if (disX > 0 || disY > 0)
        {
            if (disX < disY)
            {
                if (initialPos.y < finalPos.y)
                {
                    Debug.Log("Up");
                    if (!exitcnt)
                    {
                        Debug.Log("종료하려면 한번 더 스와이프");
                        PlayAud(this.to_main);
                        exitcnt = true;
                    }
                    else if (exitcnt)
                    {
                        Time.timeScale = 1;
                        SceneManager.LoadScene("MainScene");
                    }
                }
            }
        }
    }

    void PlayAud(AudioClip aud_clip)
    {
        this.aud.PlayOneShot(aud_clip);
    }
}
