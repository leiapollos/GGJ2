using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemy : Enemy
{
    SpriteRenderer sp;
    public int AppearSpeed;
    public float Frequency;
    Collider2D PlayerTrigger;


    new void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        PlayerTrigger = transform.Find("PlayerTrigger").GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;
        
        float Alpha = (float) ((Math.Sqrt((1 + Math.Pow(AppearSpeed,2)) / (1 + Math.Pow(AppearSpeed, 2) *
        Math.Pow(Math.Cos(timeCounter * Frequency),2))) * Math.Cos(timeCounter* Frequency)) + 1)/2;

        //Gets the color of the sprite
        Color AuxColor = sp.color;

        PlayerTrigger.enabled = Alpha > 0.7f;
        AuxColor.a = Alpha;
        sp.color = AuxColor;
    }
}
