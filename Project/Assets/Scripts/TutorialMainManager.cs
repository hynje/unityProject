using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialMainManager : MonoBehaviour
{
    private int tutorialStep = 0;
    public float m_DoubleClickSecond = 0.25f;
    private bool m_IsOneClick = false;
    private double m_Timer = 0;
    public AudioClip tutorMain1;
    public AudioClip tutorMain2;
    public AudioClip tutorMain3;
    public AudioClip tutorMain4;
    public AudioClip clear;
    AudioSource aud;
    void Start()
    {
        this.aud = GetComponent<AudioSource>();
        StartCoroutine(Tutorial());
    }

    void Update()
    {
        To_NextScene();     
    }

    IEnumerator Tutorial()
    {
        this.aud.PlayOneShot(this.tutorMain1);
        yield return new WaitForSeconds(5);
        while (tutorialStep < 1)
        {
            yield return null;
            Check();
        }
        yield return new WaitWhile(() => tutorialStep < 1);
        yield return new WaitForSeconds(1.5f);

        this.aud.PlayOneShot(this.tutorMain2);
        yield return new WaitForSeconds(5);
        while (tutorialStep < 2)
        {
            yield return null;
            Check();
        }
        yield return new WaitWhile(() => tutorialStep < 2);
        yield return new WaitForSeconds(1.5f);

        this.aud.PlayOneShot(this.tutorMain3);
        yield return new WaitForSeconds(5);
        while (tutorialStep < 3)
        {
            yield return null;
            Check();
        }
        yield return new WaitWhile(() => tutorialStep < 3);
        yield return new WaitForSeconds(1.5f);

        this.aud.PlayOneShot(this.tutorMain4);
        yield return new WaitForSeconds(4.2f);

        yield break;
    }

    void Check()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.aud.PlayOneShot(this.clear);
            tutorialStep++;
        }
    }

    void To_NextScene()
    {
        if (tutorialStep == 3)
        {
            if (m_IsOneClick && ((Time.time - m_Timer) > m_DoubleClickSecond)) { m_IsOneClick = false; }
            if (Input.GetMouseButtonDown(0))
            {
                if (!m_IsOneClick) { m_Timer = Time.time; m_IsOneClick = true; }
                else if (m_IsOneClick && ((Time.time - m_Timer) < m_DoubleClickSecond))
                {
                    m_IsOneClick = false;

                    // string userId = "CGOKnuzOP4MBTqaT7x9HlU7gIiX2"; //test UID
                    //RealtimeDatabase.Instance.chagneTutorialstate(userId);

                    RealtimeDatabase.Instance.chagneTutorialstate(LoginManager.user.UserId);
                    SceneManager.LoadScene("MainScene");

                }
            }
        }

    }
}
