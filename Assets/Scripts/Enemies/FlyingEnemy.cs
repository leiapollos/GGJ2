using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    public float Speed;
    public float DeltaY;
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;
        rb.transform.position = new Vector2(rb.transform.position.x, (float) Math.Sin(timeCounter * Speed) * DeltaY);
    }
}
