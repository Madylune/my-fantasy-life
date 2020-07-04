using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] playlist;
    public AudioSource audioSource;
    private string sceneName;

    private void Awake() 
    {
        sceneName = SceneManager.GetActiveScene().name;
    }

    void Start()
    {
        if (sceneName == "MageTown")
        {
            PlaySpecificMusic(0);
        }
        if (sceneName == "Forest03")
        {
            PlaySpecificMusic(1);
        }
    }

    void Update()
    {
        if (sceneName != SceneManager.GetActiveScene().name)
        {
            sceneName = SceneManager.GetActiveScene().name;

            if (audioSource.isPlaying)
            {
                audioSource.Stop();
                if (sceneName == "MageTown")
                {
                    PlaySpecificMusic(0);
                }
                if (sceneName == "Forest03")
                {
                    PlaySpecificMusic(1);
                }
            }
        }
    }

    void PlaySpecificMusic(int index)
    {
        audioSource.clip = playlist[index];
        audioSource.Play();
    }
}
