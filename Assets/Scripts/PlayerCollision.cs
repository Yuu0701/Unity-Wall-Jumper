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
        // On Every Load Scene, this gets set as false
        isDead = false;
    }
    private void Update()
    {
        if (!isDead)
        {
            // Collect camera positions where Player will die if they enter the range
            cameraBottomYPos = Camera.main.transform.position.y - Camera.main.orthographicSize - cameraOffset;
            cameraTopYPos = Camera.main.transform.position.y + Camera.main.orthographicSize + cameraOffset;

            // If the Player is in the range of outside of Camera frame + offset, they die
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

        // Just in case check if the gameOver object is there
        if (gameOverMenu != null)
        {
            // Activate the Game Over Menu Screen
            gameOverMenu.DisplayGameOverScreen();
        }

        Destroy(gameObject);
    }
}
