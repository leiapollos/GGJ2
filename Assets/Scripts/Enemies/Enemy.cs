using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected float Gravity = 9.8f;
    protected float timeCounter = 0;

    Player player;


    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(player);
        if (collision.tag == "Player")
        {
            player.Hit();
        }
    }
}
