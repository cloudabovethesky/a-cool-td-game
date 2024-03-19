using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void SetVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public float GetVolume()
    {
        return musicSource.volume;
    }
}
