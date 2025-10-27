using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] AudioClip death;
    [SerializeField] private GameOverMenu gameOverMenu;
    [SerializeField] private float cameraOffset = 2f;
    [SerializeField] bool isDead;

    private float cameraBottomYPos;
    private float cameraTopYPos;


    private void Awake()
    {
        Debug.Log(isDead);
        isDead = false;
    }
    private void Update()
    {
        if (!isDead)
        {
            cameraBottomYPos = Camera.main.transform.position.y - Camera.main.orthographicSize - cameraOffset;
            cameraTopYPos = Camera.main.transform.position.y + Camera.main.orthographicSize + cameraOffset;

            if ((transform.position.y < cameraBottomYPos) || (transform.position.y > cameraTopYPos))
            {
                Die();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead)
        {
            return;
        }
        
        // Player dies and audio tunes in if hit by obstacle
        if (collision.transform.tag == "Obstacle")
        {

            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        SoundManager.instance.PlaySound(death);

        if (gameOverMenu != null)
        {
            gameOverMenu.DisplayGameOverScreen();
        }

        Destroy(gameObject);
    }
}
