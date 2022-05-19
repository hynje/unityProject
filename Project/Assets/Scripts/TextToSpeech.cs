using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Xml;


public class TextToSpeech : MonoBehaviour
{
    public AudioSource audioSource;

    [SerializeField]
    private string getText;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void playAudio()
    {
        StartCoroutine(DownloadAudio(getText));
    }

    IEnumerator DownloadAudio(string info)
    {

        string language = "ko";
        string url = "http://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=32&client=tw-ob&q=" + info + "&tl=" + language;

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www;

        audioSource.clip = DownloadHandlerAudioClip.GetContent(www);
        audioSource.Play();
    }
}