using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlayer : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;
    AudioSource aud;
    Vector3 mousePosition;
    Vector3 direction;
    float movespeed = 150f;
    public AudioClip crash;
    void Start()
    {
        this.aud = GetComponent<AudioSource>();
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //��ġ�� ������ �÷��̾� �̵�
        if (Input.GetMouseButton(0))
        {
            animator.SetTrigger("TouchTrigger");    //��ġ�ϸ� �ִϸ��̼� ���
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (mousePosition - transform.position).normalized;
            rigid2D.velocity = new Vector2(direction.x * movespeed, direction.y * movespeed);
        }
        else
        {
            rigid2D.velocity = Vector2.zero;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        this.aud.PlayOneShot(this.crash);
    }
}
