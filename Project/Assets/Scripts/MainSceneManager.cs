using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    Camera cam;
    GameObject exitPanel;
    public AudioClip start;
    public AudioClip rank;
    AudioSource aud;

    public float m_DoubleClickSecond = 0.25f;
    private bool m_IsOneClick = false;
    private double m_Timer = 0;

    private Vector2 initialPos;
    private Vector3 currentPos;
    private Vector3 nextPos;

    private int exitcnt = 0;
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
        if(exitcnt == 0)
        {
            PlayGame();
        }
        
        
        if (Input.GetMouseButtonDown(0))
        {
            initialPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Swipe(Input.mousePosition);
        }
        
    }

    void PlayGame()
    {
        if (m_IsOneClick && ((Time.time - m_Timer) > m_DoubleClickSecond)) { m_IsOneClick = false; }
        if (Input.GetMouseButtonDown(0))
        {
            if (!m_IsOneClick) { m_Timer = Time.time; m_IsOneClick = true; }
            else if (m_IsOneClick && ((Time.time - m_Timer) < m_DoubleClickSecond))
            {
                m_IsOneClick = false;
                if(currentPos.x == 0)
                    SceneManager.LoadScene("GameScene");
            }
        }
    }

    void Swipe(Vector3 finalPos)
    {
        float disX = Mathf.Abs(initialPos.x - finalPos.x);
        float disY = Mathf.Abs(initialPos.y - finalPos.y);
        currentPos = cam.transform.position;
        if (disX > 0 || disY > 0)
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
                    exitcnt = 0;
                }
                else
                {
                    Debug.Log("Up");
                    if (exitcnt == 0)
                    {
                        Time.timeScale = 0f;
                        exitPanel.SetActive(true);
                        exitcnt = 1;
                    }
                    else if (exitcnt == 1)
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
