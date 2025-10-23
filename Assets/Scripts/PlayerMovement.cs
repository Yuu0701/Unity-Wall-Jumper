using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float horizontal;
    [SerializeField] private float speed = 16f;
    [SerializeField] private float jumpPower = 24f;

    // Character Art's face direction
    private bool isFacingRight = true;

    // Character Wall Sliding variables
    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    // Wall Jump Variables
    private bool isWallJumping;
    private float wallJumpDir;
    private float wallJumpTime = 0.2f; // Grace period time
    private float wallJumpCounter; // Grace period for wall jump input so that it doesnt have to be frame perfect
    [SerializeField] private float wallJumpDuration = 0.4f;
    [SerializeField] private Vector2 wallJumpPower = new Vector2(16f, 24f);

    // Double Jump Variables
    private bool doubleJump;
    private bool wasGrounded; // Check if the player was touching the ground before the double jump

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck; // Store GroundCheck GameObject's Transform information under Player GameObject
    [SerializeField] private LayerMask groundLayer; // The Ground prefab's Layer needs to be set to Ground
    [SerializeField] private Transform wallCheck; // Reference the wallCheck Gameobject's Transform
    [SerializeField] private LayerMask wallLayer; // Reference the wall Layer 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get the Horizontal input from the keyboard every frame
        horizontal = Input.GetAxisRaw("Horizontal");

        bool grounded = IsGrounded();

        if (grounded && !wasGrounded)
        {
            doubleJump = false; // Reset the double jump when touching the ground
        }
        else if (!grounded && wasGrounded)
        {
            doubleJump = true; // Player can double jump after falling from the ledge
        }

        // Jump, short jump, double jump available
        if (Input.GetButtonDown("Jump") && (IsGrounded() || doubleJump))
        {
            Jump();
            
            if (grounded)
            {
                doubleJump = true;
            }
            else
            {
                doubleJump = false;
            }
        }
        if (Input.GetButtonUp("Jump"))
        {
            ShortJump();
        }

        WallSlide();
        WallJump();

        if (!isWallJumping)
        {
            FlipCharacter(); // Check if the player art needs to be flipped
        }

        wasGrounded = grounded;
    }

    private void FixedUpdate()
    {
        if (!isWallJumping)
        {
            // Update the velocity in FixedUpdate for smoothness
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        }
    }

    /* ----------------------------Functions Regarding Ground Only --------------------*/
    // Function to Check if the Player is touching the ground
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    /* ----------------------------Functions Regarding Walls Only --------------------*/
    // Function to check if the Player is touching the wall (Layer)
    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        // If the player is touching the wall, away from ground, and is holding the Direction Inputs, start wall sliding
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            Debug.Log("WALL SLIDING TRUE");
            // Use Mathf.Clamp to force the character to not go faster than wallSlidingSpeed when sliding a wall
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            Debug.Log("WALL SLIDING False");
            isWallSliding = false;
        }
    }

    /* ----------------------------Functions Regarding Jumps --------------------*/
    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
    }

    private void ShortJump()
    {
        if (rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }

    private void WallJump()
    {
         if (isWallSliding)
        {
            isWallJumping = false;

            // Determine the jump direction based on the character art's direction facing
            wallJumpDir = -Mathf.Sign(transform.localScale.x);
            wallJumpCounter = wallJumpTime; // Reset the jump timer

            // Cancel Pending StopWallJump function in case the player touches another wall immediately after the wall jump
            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpCounter -= Time.deltaTime; // Time countdown
        }

         if (Input.GetButtonDown("Jump") && wallJumpCounter > 0f)
        {
            isWallJumping = true;
            rb.linearVelocity = new Vector2(wallJumpDir * wallJumpPower.x, wallJumpPower.y);
            wallJumpCounter = 0f; // So the Player cannot keep jumping

            // If the player is not facing the direction of the new wall jump's direction, flip it
            if (Mathf.Sign(transform.localScale.x) != wallJumpDir)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale; // Geth the transform's scale values
                localScale.x *= -1f; // Flip the sign for the values
                transform.localScale = localScale; // Update the Scale values on transform
            }

            doubleJump = true; // Allow double jump after wall jump

            Invoke(nameof(StopWallJumping), wallJumpDuration); // Set wall jumping bool to false after certain duration
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    /* ----------------------------Functions Regarding Flipping Character Art Only --------------------*/
    private void FlipCharacter()
    {
        // Flip the character's direction of the art depending on where they are walking
        if(isFacingRight && horizontal < 0 || !isFacingRight && horizontal > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale; // Geth the transform's scale values
            localScale.x *= -1f; // Flip the sign for the values
            transform.localScale = localScale; // Update the Scale values on transform
        }
    }
}
