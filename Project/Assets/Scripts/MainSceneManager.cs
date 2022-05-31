using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    Camera cam;
    GameObject exitPanel;
    public AudioClip start;
    public AudioClip tutorial;
    public AudioClip rank;
    AudioSource aud;

    public float m_DoubleClickSecond = 0.25f;
    private bool m_IsOneClick = false;
    private double m_Timer = 0;

    private Vector2 initialPos;
    private Vector3 currentPos;
    private Vector3 nextPos;

    private bool exitcnt = false;
    void Start()
    {
        cam = Camera.main;
        exitPanel = GameObject.Find("Exit");
        aud = GetComponent<AudioSource>();
        exitPanel.SetActive(false);
        currentPos = cam.transform.position;
        PlayAud(this.start);
    }

    void Update()
    {
        if(!exitcnt)
        {
            PlayGame();
        }
        
        
        if (Input.GetMouseButtonDown(0))
        {
            initialPos = Input.mousePosition;
            Debug.Log(initialPos);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Swipe(Input.mousePosition);
            Debug.Log(initialPos);
        }
        
    }

    void PlayGame()
    {
        if (m_IsOneClick && ((Time.time - m_Timer) > m_DoubleClickSecond)) { m_IsOneClick = false; }
        if (Input.GetMouseButtonDown(0))
        {
            currentPos = cam.transform.position;
            if (!m_IsOneClick) { m_Timer = Time.time; m_IsOneClick = true; }
            else if (m_IsOneClick && ((Time.time - m_Timer) < m_DoubleClickSecond))
            {
                m_IsOneClick = false;
                if (currentPos.x == 0)
                    SceneManager.LoadScene("GameScene");
                else if (currentPos.x == -7)
                    SceneManager.LoadScene("TutorialScene");
                else if (currentPos.x == 7)
                    SceneManager.LoadScene("RankingScene");
            }
        }
    }

    void Swipe(Vector3 finalPos)
    {
        float disX = Mathf.Abs(initialPos.x - finalPos.x);
        float disY = Mathf.Abs(initialPos.y - finalPos.y);
        currentPos = cam.transform.position;
        if (disX > 200 || disY > 200)
        {
            if (disX > disY)
            {
                if (initialPos.x > finalPos.x)
                {
                    Debug.Log("Left");
                    if(currentPos.x < 7)
                    {
                        nextPos = new Vector3(currentPos.x + 7, currentPos.y, currentPos.z);
                        cam.transform.position = nextPos;
                        if (currentPos.x == 0)
                            PlayAud(this.rank);
                        else
                            PlayAud(this.start);
                    }
                        
                }
                else
                {
                    Debug.Log("Right");
                    if (currentPos.x > -7)
                    {
                        nextPos = new Vector3(currentPos.x - 7, currentPos.y, currentPos.z);
                        cam.transform.position = nextPos;
                        if (currentPos.x == 0)
                            PlayAud(this.tutorial);
                        else
                            PlayAud(this.start);
                    }
                }
            }
            else
            {
                if (initialPos.y > finalPos.y)
                {
                    Debug.Log("Down");
                    Time.timeScale = 1f;
                    exitPanel.SetActive(false);
                    exitcnt = false;
                }
                else
                {
                    Debug.Log("Up");
                    if (!exitcnt)
                    {
                        Time.timeScale = 0f;
                        exitPanel.SetActive(true);
                        exitcnt = true;
                    }
                    else if (exitcnt)
                    {
#if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
#else
                        Application.Quit();
#endif
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
