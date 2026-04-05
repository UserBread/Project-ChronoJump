using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public int extraJumpsValue = 1;
    private int extraJumps;

    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        extraJumps = extraJumpsValue;
    }

    void Update()
    {
        float moveInput = 0f;
        Keyboard keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.aKey.isPressed) moveInput = -1f;
            if (keyboard.dKey.isPressed) moveInput = 1f;
        }
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
            else if (extraJumps > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                extraJumps--;
            }
        }
        SetAnimation(moveInput);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void SetAnimation(float moveInput)
    {
        if (isGrounded)
        {
            if (moveInput == 0)
            {
                animator.Play("playerIdle");
            }
            else
            {
                animator.Play("playerRun");
            }
        }
        else
        {
            if (rb.linearVelocity.y > 0)
            {
                animator.Play("playerJump");
            }
            else
            {
                animator.Play("playerFall");
            }
        }
    }
}