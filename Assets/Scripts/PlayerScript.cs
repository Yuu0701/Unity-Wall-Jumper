using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float wallJumpForce = 10f;
    [SerializeField] private float moveSpeed = 10f;

    [SerializeField] private float wallStickTime = 0.2f;

    [SerializeField] private int maxJumps = 2;

    private bool isGrounded = false;
    private bool isOnWall; // Checks if the player is on any wall
    private bool isLeftWall; // Check is the player is on the left wall
    private float wallStickTimer; // stores how long the player is sticking to the wall
    private int jumpsLeft;
    private bool jumpPressed; // Check if the user pressed the Input Jump button

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        isGrounded = true;
        jumpsLeft = maxJumps; // Initialize available jumps
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        // If the Input is pressed, change the boolean value
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            jumpPressed = true;
            Debug.Log("Jump Pressed!");
        }
    }

    private void FixedUpdate()
    {
        Jump();
        WallStick();
    }

    // Function that allows the character to stick to the wall
    private void WallStick()
    {
        if (isOnWall)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0f;
        }
        else
        {
            rb.gravityScale = 2f;
        }
    }

    // Handles Player's Jump mechanics
    private void Jump()
    {
        float direction; // Determine the direction of jump based on which wall the player is on

        // If the Jump button is not pressed, return it
        if (!jumpPressed)
        {
            return;
        }

        if (isGrounded) // Player can jump if is standing on the ground
        {
            rb.linearVelocity = new Vector2(moveSpeed, jumpForce);
            jumpsLeft--;
            Debug.Log("Jumped from Ground!");
        }
        else if (isOnWall) // Player can jump if on the wall
        {
            direction = playerOnLeftWall(isLeftWall);
            rb.linearVelocity = new Vector2(direction * moveSpeed, jumpForce);

            isOnWall = false;
            jumpsLeft--;
        }
        else if (jumpsLeft > 0)
        {
            direction = playerOnLeftWall(isLeftWall);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpsLeft--;
        }
        jumpPressed = false;
    }

    private float playerOnLeftWall(bool isLeftWall)
    {
        float direction;
        if (isLeftWall)
        {
            direction = 1f;
        }
        else
        {
            direction = -1f;
        }
        return direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall1"))
        {
            isOnWall = true;
            
            // If the wall exists on less x-coordinate than the player, then the wall is the left wall
            isLeftWall = collision.transform.position.x < transform.position.x;
            jumpsLeft = maxJumps; // Reset the Double Jump after sticking to the wall
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpsLeft = maxJumps; // Reset the Double Jump after sticking to the ground
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall1"))
        {
            isOnWall = false;
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
