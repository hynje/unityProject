using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class test : MonoBehaviour
{
    public AudioSource audioSource;
    public Text SoundText;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        SoundText = gameObject.GetComponentInChildren<Text>();
        Debug.LogError("soundText : " + SoundText);
    }

    /*IEnumerator TTsplay()
    {
        string url = "http://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=32&client=tw-ob&q=" + SoundText.text + "&tl=ko";
        WWW www = new WWW(url);
        yield return www;

        audioSource.clip = www.GetAudioClip(false, true, AudioType.MPEG);
        audioSource.Play();
    }*/

    public void OnClickTouch()
    {
        //StartCoroutine(TTsplay());
    }

}
