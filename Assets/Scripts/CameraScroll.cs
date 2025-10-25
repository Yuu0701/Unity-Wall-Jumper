using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 2f;

    // Update is called once per frame
    void Update()
    {
        // Move the camera up while being fps independent
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
    }
}
