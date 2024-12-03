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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (handTransform == null)
        {
            Debug.LogError("Player hand transform is not assigned!");
        }

        Cameracontroller cameracontroller = Camera.main.GetComponent<Cameracontroller>();
        if (cameracontroller != null)
        {
            //cameracontroller.Player = transform;
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
            else if (canDoubleJump) // Check if double jump is allowed
            {
                Jump(doubleJumpForce);
                canDoubleJump = false;
                isDoubleJumping = true;
            }
        }


        float move = Input.GetAxis("Horizontal");
        transform.Translate(move * moveSpeed * Time.deltaTime, 0, 0);


        UpdateAnimationState();

    /*    if (Input.GetKeyDown(KeyCode.E))  // 'E' for "use" item
        {
            UseEquippedItem();
        }
*/
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
/*
    public void EquipItem(InventoryItem item)
    {
        if (equippedItem != null)
        {
            Destroy(equippedItem);
        }

        // Instantiate the new item in the player's hand
        equippedItem = Instantiate(item.gameObject, handTransform.position, Quaternion.identity, handTransform);

        // Adjust item's transform or any other properties if needed
        equippedItem.transform.localPosition = Vector3.zero;
    }

    public void UseEquippedItem()
    {
        if (equippedItem != null)
        {
            InventoryItem itemComponent = equippedItem.GetComponent<InventoryItem>();

            if (itemComponent != null)
            {
                itemComponent.UseItem();
            }
        } 
    } 
    */
}
