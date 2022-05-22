using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;


public class buttonTTS : MonoBehaviour
{
    public static string buttonname, buttontext;

    public AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    IEnumerator DownloadAudio()
    {
        string URL = "http://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=32&client=tw-ob&q=" + buttontext + "&tl=ko";

        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(URL, AudioType.MPEG))
        {
            yield return www.SendWebRequest();


            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                audioSource.clip = DownloadHandlerAudioClip.GetContent(www);
            }

            audioSource.Play();
        }

    }

    public void GetbtnText() 
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        buttonname = clickObject.name;
        buttontext = clickObject.GetComponentInChildren<Text>().text;
        Debug.Log(buttontext);
    }

    public void ClickBtn()
    {
        GetbtnText();
        StartCoroutine(DownloadAudio());
    }
}

