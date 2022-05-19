using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public float m_DoubleClickSecond = 0.25f;
    private bool m_IsOneClick = false;
    private double m_Timer = 0;
    private Vector2 initialPos;

    public AudioClip game_over;
    public AudioClip play_again;
    AudioSource aud;
    void Start()
    {
        this.aud = GetComponent<AudioSource>();
        this.aud.PlayOneShot(this.game_over);
        Invoke("PlayAud", 2f);

        //string userId = "CGOKnuzOP4MBTqaT7x9HlU7gIiX2"; //test UID
        //RealtimeDatabase.Instance.saveScore(userId);

        RealtimeDatabase.Instance.saveScore(LoginManager.user.UserId);        
}

void Update()
    {
        PlayAgain();
        
        if (Input.GetMouseButtonDown(0))
        {
            initialPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Swipe(Input.mousePosition);
        }
    }

    void PlayAgain()
    {
        if (m_IsOneClick && ((Time.time - m_Timer) > m_DoubleClickSecond)) { m_IsOneClick = false; }
        if (Input.GetMouseButtonDown(0))
        {
            if (!m_IsOneClick) { m_Timer = Time.time; m_IsOneClick = true; }
            else if (m_IsOneClick && ((Time.time - m_Timer) < m_DoubleClickSecond))
            {
                m_IsOneClick = false;
                SceneManager.LoadScene("GameScene");
            }
        }
    }

    void Swipe(Vector3 finalPos)
    {
        float disX = Mathf.Abs(initialPos.x - finalPos.x);
        float disY = Mathf.Abs(initialPos.y - finalPos.y);
        if (disX > 200 || disY > 200)
        {
            if (disX < disY)
            {
                if (initialPos.y > finalPos.y)
                {
                    Debug.Log("Down");
                }
                else
                {
                    Debug.Log("Up");
                    SceneManager.LoadScene("MainScene");
                }
            }
        }
    }

    void PlayAud()
    {
        this.aud.PlayOneShot(this.play_again);
    }
    
}
