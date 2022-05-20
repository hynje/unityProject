using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RankingSceneManager : MonoBehaviour
{
    Camera cam;
    GameObject all_1_panel;
    GameObject all_2_panel;
    GameObject personal_panel;


    private Vector2 initialPos;
    private Vector3 currentPos;
    private Vector3 nextPos;

    private bool exitcnt = false;
    void Start()
    {
        cam = Camera.main;
        all_1_panel = GameObject.Find("all_1_panel");
        all_2_panel = GameObject.Find("all_2_panel");
        personal_panel = GameObject.Find("personal_panel");

        all_1_panel.SetActive(true);
        all_2_panel.SetActive(false);
        personal_panel.SetActive(false);
        currentPos = cam.transform.position;
    }

    void Update()
    {
        if (!exitcnt)
        {
            //PlayGame();
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
                    if (currentPos.x < 7)
                    {
                        nextPos = new Vector3(currentPos.x + 7, currentPos.y, currentPos.z);
                        cam.transform.position = nextPos;
                        if (currentPos.x == 0)
                        {
                            all_1_panel.SetActive(false);
                            all_2_panel.SetActive(true);
                            personal_panel.SetActive(false);
                        }

                        else
                        {
                            all_1_panel.SetActive(true);
                            all_2_panel.SetActive(false);
                            personal_panel.SetActive(false);
                        }


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
                        {
                            all_1_panel.SetActive(false);
                            all_2_panel.SetActive(false);
                            personal_panel.SetActive(true);
                        }

                        else
                        {
                            all_1_panel.SetActive(true);
                            all_2_panel.SetActive(false);
                            personal_panel.SetActive(false);
                        }
                    }
                }
            }
            else
            {
                if (initialPos.y > finalPos.y)
                {
                    Debug.Log("Down");
                    Time.timeScale = 1f;
                    
                    exitcnt = false;
                }
                else
                {
                    Debug.Log("Up");
                    if (!exitcnt)
                    {
                        Time.timeScale = 1f;                     
                        exitcnt = true;
                        

                    }
                    else if (exitcnt)
                    {
                        SceneManager.LoadScene("MainScene");
                    }
                }
            }
        }
    }
}



