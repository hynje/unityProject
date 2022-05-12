using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    private int tutorialStep;
    public float m_DoubleClickSecond = 0.25f;
    private bool m_IsOneClick = false;
    private double m_Timer = 0;
    [SerializeField]
    private MapData mapData;
    [SerializeField]
    private GameObject enemyPrefab1;
    [SerializeField]
    private GameObject enemyPrefab2;
    [SerializeField]
    private GameObject enemyPrefab3;

    public AudioClip start;
    public AudioClip toMain;
    public AudioClip clear;
    public AudioClip try_again;
    public AudioClip step1;
    public AudioClip step2;
    public AudioClip step3;
    public AudioClip step4;
    AudioSource aud;
    void Start()
    {
        this.aud = GetComponent<AudioSource>();
        this.aud.PlayOneShot(this.start);
        tutorialStep = 0;
        StartCoroutine(Tutorial());
    }

    void Update()
    {
          To_NextScene();
    }

    IEnumerator Tutorial()
    {

        yield return new WaitForSeconds(1.5f);
        this.aud.PlayOneShot(this.step1);
        yield return new WaitForSeconds(10);
        while (tutorialStep < 1)
        {
            yield return null;
            Step_Move();
        }
        yield return new WaitWhile(() => tutorialStep < 1);

        yield return new WaitForSeconds(2);
        this.aud.PlayOneShot(this.step2);
        yield return new WaitForSeconds(4.5f);
        GameObject enemyPrefab = Instantiate(enemyPrefab1, new Vector3(0, mapData.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);
        while (tutorialStep < 2)
        {
            yield return null;
            if (!enemyPrefab)
            {
                this.aud.PlayOneShot(try_again);
                yield return new WaitForSeconds(1);
                enemyPrefab = Instantiate(enemyPrefab1, new Vector3(0, mapData.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);
            }
            Step_Enemy(enemyPrefab);
        }
        yield return new WaitWhile(() => tutorialStep < 2);

        yield return new WaitForSeconds(2);
        this.aud.PlayOneShot(this.step3);
        yield return new WaitForSeconds(3);
        enemyPrefab = Instantiate(enemyPrefab2, new Vector3(0, mapData.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);
        while (tutorialStep < 3)
        {
            yield return null;
            if (!enemyPrefab)
            {
                this.aud.PlayOneShot(try_again);
                yield return new WaitForSeconds(1);
                enemyPrefab = Instantiate(enemyPrefab2, new Vector3(0, mapData.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);
            }
            Step_Enemy(enemyPrefab);
        }
        yield return new WaitWhile(() => tutorialStep < 3);

        yield return new WaitForSeconds(2);
        this.aud.PlayOneShot(this.step4);
        yield return new WaitForSeconds(4);
        enemyPrefab = Instantiate(enemyPrefab3, new Vector3(0, mapData.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);
        while (tutorialStep < 4)
        {
            yield return null;
            if (!enemyPrefab)
            {
                this.aud.PlayOneShot(try_again);
                yield return new WaitForSeconds(1);
                enemyPrefab = Instantiate(enemyPrefab3, new Vector3(0, mapData.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);
            }
            Step_Enemy(enemyPrefab);
        }
        yield return new WaitWhile(() => tutorialStep < 4);

        yield return new WaitForSeconds(2);
        this.aud.PlayOneShot(this.toMain);
        yield return new WaitForSeconds(5);
        yield break;
    }

    void Step_Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.aud.PlayOneShot(this.clear);
            tutorialStep++;
        }
    }

    void Step_Enemy(GameObject enemyPrefab)
    {
        float pos = 5;
        if(enemyPrefab)
            pos = enemyPrefab.transform.position.y;
        if (pos < -4.9)
        {
            this.aud.PlayOneShot(clear);
            tutorialStep++;
        }
        
    }

    void To_NextScene()
    {
        if (tutorialStep == 4)
        {
            if (m_IsOneClick && ((Time.time - m_Timer) > m_DoubleClickSecond)) { m_IsOneClick = false; }
            if (Input.GetMouseButtonDown(0))
            {
                if (!m_IsOneClick) { m_Timer = Time.time; m_IsOneClick = true; }
                else if (m_IsOneClick && ((Time.time - m_Timer) < m_DoubleClickSecond))
                {
                    m_IsOneClick = false;
                    SceneManager.LoadScene("Tutorial_MainScene");
                }
            }
        }
            
    }
}
