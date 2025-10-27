using UnityEngine;

public class BlueBallMovement : MonoBehaviour
{
    [SerializeField] private Vector2 velocity;
    [SerializeField] private Rigidbody2D rb;

    // Reference the wallCheck Gameobject's Transform
    [SerializeField] private Transform wallCheckLeft; 
    [SerializeField] private Transform wallCheckRight;

    [SerializeField] private LayerMask wallLayer; // Reference the wall Layer

    private float direction;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Random.Range(0, 2) == 0)
        {
            direction = -1f;
        }
        else
        {
            direction = 1f;
        }

        velocity = new Vector2(direction * 10f, -2f);
        rb.linearVelocity = velocity;
        rb.gravityScale = 0; // Set gravity to 0 for constant velocity
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.linearVelocity;

        // Change direction if touching the wall
        if (IsWalled())
        {
            velocity *= -1;

        }
    }

    private bool IsWalled()
    {
       // Return true if either wall checker detects touching the wall Layer
        return Physics2D.OverlapCircle(wallCheckLeft.position, 0.2f, wallLayer) || Physics2D.OverlapCircle(wallCheckRight.position, 0.2f, wallLayer);
    }
}
