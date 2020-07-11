using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemy : Enemy
{
    public bool IsGrounded;
    public float GroundTestLength = 0.1f;
    Transform feet;
    public float FeetWidth = 1;
    public float JumpHeight = 1;
    public float Speed;

    new void Start()
    {
        base.Start();
        feet = transform.Find("Feet");
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded = Physics2D.BoxCast(feet.position, new Vector2(FeetWidth, 0.001f), 0, Vector2.down, GroundTestLength, LayerMask.GetMask("Ground"));

        if (IsGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sqrt(JumpHeight * 2 * Gravity));
        }
        if (rb.velocity.y > 0)
        {
            rb.velocity += Vector2.down * Gravity * Time.deltaTime;
        }
        else
        {
            rb.velocity += Vector2.down * Gravity * Time.deltaTime;
        }

        //Runs Toward Player
        rb.velocity = new Vector2(-Speed, rb.velocity.y);
    }
}
