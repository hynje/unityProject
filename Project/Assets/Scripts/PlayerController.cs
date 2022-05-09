using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private MapData mapData;
    Rigidbody2D rigid2D;
    Animator animator;
    AudioSource aud;
    Vector3 mousePosition;
    Vector3 direction;
    float movespeed = 150f;
    void Start()
    {
        aud = GetComponent<AudioSource>();
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
        
        Debug.Log("GameOver");
        SceneManager.LoadScene("GameOverScene");
        

    }
}
