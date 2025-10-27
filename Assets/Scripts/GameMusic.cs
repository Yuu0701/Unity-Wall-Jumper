using UnityEngine;

public class GameMusic : MonoBehaviour
{
    

    private void Awake()
    {
        // New GameObject will get created on every Scene Load
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Music");
        
        // If more than 1 gameobject is found, delete the new gameobject and keep the original
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
