using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float verticalOffset = 2f;   // How far ahead of the player the camera should look
    public float smoothSpeed = 5f;      // Smooth camera movement

    private float highestY;             // Track the highest Y position the camera reached

    void Start()
    {
        highestY = transform.position.y;
    }

    void LateUpdate()
    {
        if (player.position.y + verticalOffset > highestY)
        {
            highestY = player.position.y + verticalOffset;
        }

        // Smoothly move the camera
        Vector3 targetPos = new Vector3(transform.position.x, highestY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
    }
}
                                                                    