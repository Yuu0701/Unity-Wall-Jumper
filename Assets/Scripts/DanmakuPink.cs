using UnityEngine;

public class DanmakuPink : MonoBehaviour
{
    [SerializeField] private Vector2 velocity = new Vector2(10f, -2f);
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform wallCheck; // Reference the wallCheck Gameobject's Transform
    [SerializeField] private LayerMask wallLayer; // Reference the wall Layer
    [SerializeField] private float bounceCooldown = 0.1f;

    private float lastBounceTime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = velocity;
        rb.gravityScale = 0; // Set gravity to 0 for constant velocity
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.linearVelocity;
        if (IsWalled())
        {
            velocity *= -1;
            
        }
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }
}
