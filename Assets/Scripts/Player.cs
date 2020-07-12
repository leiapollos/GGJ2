using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public bool IsJumping;
    public bool IsGrounded;
    public bool GoingRight = true;
    public float Speed = 5;
    public float Gravity = 9.8f;
    public float FallMultiplier = 2;
    public float JumpHeight = 1;
    public float FeetWidth = 1;
    public float GroundTestLength = 0.1f;
    public int lives = 3;
    public int MaxStep = 5;
    
    public AudioPlayer sounds;
    System.Random rand = new System.Random();

    Rigidbody2D rb;
    Transform feet;

    [HideInInspector]
    public Vector2 velocity;
    Vector2 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        feet = transform.Find("Feet");
        lastPos = rb.position;
        sounds = GetComponent<AudioPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Jump
        IsGrounded = Physics2D.BoxCast(feet.position, new Vector2(FeetWidth, 0.001f), 0, Vector2.down, GroundTestLength, LayerMask.GetMask("Ground"));

        if (IsGrounded && Input.GetButtonDown("Jump"))
        {
            IsJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sqrt(JumpHeight * 2 * Gravity));
        }
        if (Input.GetButtonUp("Jump") && IsJumping)
        {
            IsJumping = false;
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        if (rb.velocity.y > 0)
        {
            rb.velocity += Vector2.down * Gravity * Time.deltaTime;
        }
        else
        {
            IsJumping = false;
            rb.velocity += Vector2.down * Gravity * FallMultiplier * Time.deltaTime;
        }

        //Horizontal Movement

        rb.velocity = new Vector2(GoingRight ? Speed : -Speed, rb.velocity.y);

    }

    void FixedUpdate()
    {
        velocity = (rb.position - lastPos) / Time.fixedDeltaTime;
        lastPos = rb.position;
    }

    public void Hit()
    {
        lives--;
        if (lives <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void PlayStep()
    {
        if (IsGrounded)
        {
            string dimension = LevelManager.main.dimensionName;
            int indexStep = rand.Next(1, MaxStep + 1);
            sounds.PlayOnce(dimension+ "Step" + indexStep);
        }

    }



}
