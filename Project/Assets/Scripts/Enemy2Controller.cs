using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : MonoBehaviour
{
    public AudioClip enemy2SE;
    AudioSource aud;
    GameObject player;
    float moveSpeed = 2.0f;
    Vector2 moveDirection;
    void Start()
    {
        this.aud = GetComponent<AudioSource>();
        this.player = GameObject.Find("Player");
        Vector2 targetPos = this.player.transform.position;
        Vector2 myPos = transform.position;
        moveDirection = (targetPos - myPos).normalized;

        //�÷��̾ �ִ� �������� ȸ��
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

        this.aud.Play();
    }

    void Update()
    {
        //�ٶ󺸴� �������� ����
        transform.Translate(new Vector2(0, 1) * moveSpeed * Time.deltaTime);

        //ȭ�� ������ ������ ����
        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }

        //�Ͻ������� �Ҹ� ����
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
