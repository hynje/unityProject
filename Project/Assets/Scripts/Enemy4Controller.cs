using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4Controller : MonoBehaviour
{
    //public AudioClip enemy4SE;
    AudioSource aud;
    GameObject player;
    int delay = 20;
    float time;
    float moveSpeed = 7.0f;
    float move_xPos;
    Queue<float> player_xPos;

    void Start()
    {
        this.aud = GetComponent<AudioSource>();
        this.player = GameObject.Find("Player");
        player_xPos = new Queue<float>();

        this.aud.Play();
    }

    void Update()
    {
        time += Time.deltaTime;
        if(time <= 3)
        {
            //�÷��̾� ����ٴϸ� �̵�
            player_xPos.Enqueue(player.transform.position.x);
            if (player_xPos.Count > delay)
            {
                move_xPos = player_xPos.Dequeue();
            }
            MovePositoin(new Vector3(move_xPos, 5.0f, 0f));
        }
        else //3���� �߻�
        {
            transform.Translate(new Vector2(0, -1) * moveSpeed * Time.deltaTime);
        }

        //ȭ�� ������ ������ ����
        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }
    }

   
   private void MovePositoin(Vector3 newPos)
    {
        transform.position = newPos;
    }
}
