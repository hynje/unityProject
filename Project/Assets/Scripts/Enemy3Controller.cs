using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Controller : MonoBehaviour
{
    public AudioClip enemy3SE;
    AudioSource aud;
    GameObject player;
    float moveSpeed = 1.6f;
    float frequency = 5.0f;
    float height = 0.1f;
    Vector2 moveDirection;
    void Start()
    {
        this.aud = GetComponent<AudioSource>();
        this.player = GameObject.Find("Player");
        Vector2 targetPos = this.player.transform.position;
        Vector2 myPos = transform.position;
        moveDirection = (targetPos - myPos).normalized;

        //플레이어가 있는 방향으로 회전
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

        this.aud.Play();
    }

    void Update()
    {
        //진동하며 이동
        if(Time.deltaTime > 0)
        {
            transform.Translate(new Vector2(0, 1) * moveSpeed * Time.deltaTime);
            transform.position = transform.position + transform.right * Mathf.Sin(Time.time * frequency) * height;
        }
        
        //화면 밖으로 나가면 삭제
        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }

        //일시정지시 소리 멈춤
        if (Time.timeScale == 0f)
        {
            this.aud.Pause();
        }
        else
        {
            this.aud.UnPause();
        }
    }
}
