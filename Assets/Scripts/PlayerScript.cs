using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float wallJumpForce = 10f;
    [SerializeField] private float moveSpeed = 6f;

    [SerializeField] private float wallStickTime = 0.2f;

    [SerializeField] private int maxJumps = 2;

    private bool isGrounded = false;
    private bool isRightWall;
    private bool isLeftWall;
    private float wallStickTimer;
    private int jumpsLeft;
    private bool jumpPressed;

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
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            jumpPressed = true;
            Debug.Log("Jump Pressed!");
        }
    }

    private void FixedUpdate()
    {
        Jump();
    }

    // Handles Player's Jump mechanics
    private void Jump()
    {
        // If the Jump button is not pressed, return it
        if (!jumpPressed)
        {
            return;
        }

        rb.linearVelocity = new Vector2(moveSpeed, jumpForce);
        jumpPressed = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall1"))
        {
            isRightWall = true;
            isLeftWall = collision.transform.position.x < transform.position.x;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall1"))
        {
            isRightWall = false;
        }
    }
}
