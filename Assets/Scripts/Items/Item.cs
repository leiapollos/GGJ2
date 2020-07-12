using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected Player player;

    protected AudioPlayer sounds;

    protected void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        sounds = GetComponent<AudioPlayer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Grab();
            Destroy(this.gameObject);
        }

    }

    public abstract void Grab();


}
