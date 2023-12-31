using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    [SerializeField] private LayerMask jumpableGround;


    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState{ idle, running , jumping , falling };

    [SerializeField] private AudioSource junpingSoundEffect;


    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.anim = GetComponent<Animator>();
        this.sprite = GetComponent<SpriteRenderer>();
        this.coll = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {


        this.dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);


        if (Input.GetButtonDown("Jump") && IsGrounded()){
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            junpingSoundEffect.Play();
        }

        UpdateAnimationState();


        if (Input.GetButtonDown("Quit"))
        {
            Application.Quit();
        }





    }

    private void UpdateAnimationState()
    {

        MovementState state = MovementState.idle;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else if (dirX == 0f)
        {
            state = MovementState.idle;
        }

        if(rb.velocity.y > 0.1)
        {
            state = MovementState.jumping;
        }else if(rb.velocity.y < -0.1)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state",(int)state);


    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f,jumpableGround);
    }


}
