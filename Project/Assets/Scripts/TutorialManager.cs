using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private int tutorialStep;
    void Start()
    {
        tutorialStep = 0;
        StartCoroutine(Tutorial());
    }

    void Update()
    {
        
    }

    IEnumerator Tutorial()
    {

        Debug.Log("Step 1");
        while (tutorialStep < 1)
        {
            yield return null;
            Step1();
        }
        yield return new WaitWhile(() => tutorialStep < 1);

        Step2();
        yield return new WaitWhile(() => tutorialStep < 2);
    }

    void Step1()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Well Done!");
            tutorialStep++;
        }
    }

    void Step2()
    {
        Debug.Log("Step 2");
        tutorialStep++;
        return;
    }
}
