using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    private static TextMeshProUGUI UITEXT;
    private static int currentScore = 0;


    private void Awake()
    {
        UITEXT = GetComponent<TextMeshProUGUI>();

        // If the PlayerPrefs already exists, read it
        if (PlayerPrefs.HasKey("HighScore"))
        {
            currentScore = PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            currentScore = 0;
            PlayerPrefs.SetInt("HighScore", currentScore);
        }
    }

    static public int SCORE
    {
        get { return currentScore; }
        private set
        {
            currentScore = value;
            PlayerPrefs.SetInt("HighScore", value);

            if (UITEXT != null)
            {
                UITEXT.text = "High Score: " + value.ToString("#,0") + "m";
            }
        }
    }

    static public void SetHighScore(int scoreToSet)
    {
        if (scoreToSet <= SCORE)
        {
            return;
        }
        SCORE = scoreToSet;
    }
}
