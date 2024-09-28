using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviourPun
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float doubleJumpForce = 7f; // Add double jump force

    private bool canDoubleJump = false; // Track if double jump is allowed
    private bool isDoubleJumping = false; // Track if double jump is in progress

    private int atkDamage = 10;

    private enum MovementState { idle, running, jumping, falling, doubleJ }
    private MovementState state = MovementState.idle;

    [SerializeField] private AudioSource jumpSoundEffect;
    private bool raceFinished = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (GameManager.Instance.isMultiplayer)
        {
            if (photonView.IsMine)
            {
                Cameracontroller cameracontroller = Camera.main.GetComponent<Cameracontroller>();
                if (cameracontroller != null)
                {
                    cameracontroller.Player = transform;
                }
            }
            Debug.Log(photonView.IsMine);
        }
        else
        {
            // Single-player setup
            Cameracontroller cameracontroller = Camera.main.GetComponent<Cameracontroller>();
            if (cameracontroller != null)
            {
                cameracontroller.Player = transform;
            }
        }
    }

    private void Update()
    {
                if (!raceFinished)
        {
            if (GameManager.Instance.isMultiplayer)
            {
                if (photonView.IsMine)
                {
                    HandleMovement();
                }
            }
            else
            {
                HandleMovement();
            }
        }
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                Jump(jumpForce);
                canDoubleJump = true; // Reset double jump ability if grounded
                isDoubleJumping = false;
            }
            else if (canDoubleJump) // Check if double jump is allowed
            {
                Jump(doubleJumpForce); // Use double jump force
                canDoubleJump = false; // Prevent further double jumps until grounded again
                isDoubleJumping = true;
            }
        }

        if (photonView.IsMine)
        {
            float move = Input.GetAxis("Horizontal");
            transform.Translate(move * moveSpeed * Time.deltaTime, 0, 0);
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

        // Check if double jumping to set animation accordingly
        if (isDoubleJumping)
        {
            state = MovementState.jumping;
        }

        anim.SetInteger("state", (int)state);
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
