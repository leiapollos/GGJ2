using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    public float Speed;
    public float DeltaY;

    private float StartY;
    new void Start()
    {
        base.Start();
        StartY = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeCounter += Time.fixedDeltaTime;
        rb.MovePosition(new Vector2(rb.transform.position.x, StartY + (float) Math.Sin(timeCounter * Speed) * DeltaY));
    }
}
