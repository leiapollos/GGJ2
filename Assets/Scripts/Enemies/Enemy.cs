﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected float Gravity = 9.8f;
    protected float timeCounter = 0;


    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}
