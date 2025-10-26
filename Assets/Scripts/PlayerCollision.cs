using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] AudioClip death;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            SoundManager.instance.PlaySound(death);
            Destroy(gameObject);
        }
    }
}
