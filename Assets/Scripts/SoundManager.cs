using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; } // Reference to the SoundManager

    private AudioSource source;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        source = GetComponent<AudioSource>();
    }

    // Play the Audio clip with no Loop
    public void PlaySound(AudioClip musicSound)
    {
        source.PlayOneShot(musicSound);
    }
}
