using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 2f;
    [SerializeField] private float timer;
    [SerializeField] private float timeUntilSpeedUp = 5f;
    [SerializeField] private float maxScrollSpeed = 6f;
    [SerializeField] private float speedIncrementer = 0.5f;

    // Update is called once per frame
    void Update()
    {
        // Move the camera up while being fps independent
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);

        timer += Time.deltaTime;

        // Gradually increase scroll speed until maxScrollSpeed is met
        if (timer >= timeUntilSpeedUp && scrollSpeed < maxScrollSpeed)
        {
            scrollSpeed += speedIncrementer;
            timer = 0f;
        }
    }
}
