using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] private Transform centerBackground; // Transform reference of center background (parent)

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= centerBackground.position.y + 41.41667f)
        {
            centerBackground.position = new Vector2(centerBackground.position.x, transform.position.y + 41.41667f);
        }
        else if (transform.position.y <= centerBackground.position.y - 41.41667f)
        {
            centerBackground.position = new Vector2(centerBackground.position.x, transform.position.y - 41.41667f);
        }
    }
}
