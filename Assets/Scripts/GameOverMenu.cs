using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenuUI;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private Score _score;
    [SerializeField] AudioClip gameOverAudio;

    private void Start()
    {
        // The Game Over Screen should be off at the start of the game
        gameOverMenuUI.SetActive(false);
    }

    public void DisplayGameOverScreen()
    {
        SoundManager.instance.PlaySound(gameOverAudio);
        gameOverMenuUI.SetActive(true);

        // Save the scores on the Highest Score
        _score.SaveHighScore();

        // Update the score and high score display on GameOver Screen
        finalScoreText.text = "Your Score: " + _score.getCurrentScore().ToString("#,0") + "m";
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString("#,0") + "m";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
