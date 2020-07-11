using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunEnemy : Enemy
{
    public bool IsGrounded;
    public float GroundTestLength = 0.1f;
    public float Speed;
    Transform feet;
    public float FeetWidth = 1;

    new void Start()
    {
        base.Start();
        feet = transform.Find("Feet");
    }

    void Update()
    {
        IsGrounded = Physics2D.BoxCast(feet.position, new Vector2(FeetWidth, 0.001f), 0, Vector2.down, GroundTestLength, LayerMask.GetMask("Ground"));

        //When the enemy spawns in the air
        if (!IsGrounded)
        {
            rb.velocity += Vector2.down * Gravity * Time.deltaTime;
        }

        //Runs Toward Player
        rb.MovePosition(new Vector2(-Speed, rb.velocity.y));
    }
}
