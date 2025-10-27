using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textElement;
    public Transform player;
    private int score;
    private float highestYPos; // Highest Y position of Player
    private float playerStartingY;

    private void Start()
    {
        textElement = GetComponent<TextMeshProUGUI>();
        highestYPos = player.position.y;
        playerStartingY = player.position.y;
    }

    private void Update()
    {
        // Increase the score based on Player's Y position
        if (player.position.y > highestYPos)
        {
            highestYPos = player.position.y;
            score = Mathf.FloorToInt(highestYPos - playerStartingY);
        }

        textElement.text = score.ToString("#,0") + "m";
    }

    public int getCurrentScore()
    {
        return score;
    }

    // Calls HighScore Script to try and store a new High Score
    public void SaveHighScore()
    {
        HighScore.SetHighScore(score);
    }
}
