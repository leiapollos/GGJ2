using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    float timeCounter = 0;
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;
        rb.transform.position = new Vector2(rb.velocity.x, (float) Math.Sin(timeCounter)*Speed);
    }
}
