using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    public Transform handTransform;  // The point where items will appear in the player's hand
    private GameObject equippedItem;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float doubleJumpForce = 7f;

    public Animator secondPlayerAnimator; // Animator for the second player
    private Animator currentAnimator; // Tracks the active player's animator

    private bool canDoubleJump = false; 
    private bool isDoubleJumping = false; 

    private int atkDamage = 10;
    private float dirX = 0f;

    private enum MovementState { idle, running, jumping, falling, doubleJ }
    private MovementState state = MovementState.idle;

    [SerializeField] private AudioSource jumpSoundEffect;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        currentAnimator = anim; // Initially, the first player's animator is active

        if (handTransform == null)
        {
            Debug.LogError("Player hand transform is not assigned!");
        }
    }

    private void Update()
    {
        HandleMovement();

        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                Jump(jumpForce);
                canDoubleJump = true;
                isDoubleJumping = false;
            }
            else if (canDoubleJump)
            {
                Jump(doubleJumpForce);
                canDoubleJump = false;
                isDoubleJumping = true;
            }
        }

        UpdateAnimationState();
    }

    void HandleMovement()
    {
        float move = Input.GetAxis("Horizontal");
        transform.Translate(move * moveSpeed * Time.deltaTime, 0, 0);
    }

    private void UpdateAnimationState()
    {
        MovementState state;
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
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        if (isDoubleJumping)
        {
            state = MovementState.jumping;
        }

        // Update the current animator based on the state
        if (currentAnimator != null)
        {
            currentAnimator.SetInteger("state", (int)state);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void Jump(float jumpForce)
    {
        jumpSoundEffect.Play();
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void SwitchAnimator(Animator newAnimator)
    {
        currentAnimator = newAnimator;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(atkDamage);
            }
        }
    }
}
