using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public static AudioManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
            }
            return instance;
        }
    }

    public AudioClip[] playlist;
    public AudioSource audioSource;
    private string sceneName;

    public AudioMixerGroup soundEffectMixer;

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

    public AudioSource PlayClipAt(AudioClip clip, Vector3 pos)
    {
        // Create a temporary empty Game Object
        GameObject tempGO = new GameObject("TempAudio");

        tempGO.transform.position = pos;

        // Add a component AudioSource to the GameObject
        AudioSource audioSource = tempGO.AddComponent<AudioSource>();

        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = soundEffectMixer;
        audioSource.Play();

        // Destroy this GO after playing sound
        Destroy(tempGO, clip.length);

        return audioSource;
    }
}
