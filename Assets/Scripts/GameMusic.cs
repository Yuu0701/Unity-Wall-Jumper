using UnityEngine;

public class GameMusic : MonoBehaviour
{
    

    private void Awake()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Music");

        if (obj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            // Dont destroy the music when the game replays
            DontDestroyOnLoad(gameObject);
        }
    }
}
