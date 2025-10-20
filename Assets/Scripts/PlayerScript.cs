using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float wallJumpForce = 10f;
    [SerializeField] private float moveSpeed = 6f;

    private bool isGrounded = false;
    private bool isRightWall;
    private bool isLeftWall;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
