using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioManager audioManager;

    private void Start()
    {
        volumeSlider.value = audioManager.GetVolume();
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    private void SetVolume(float volume)
    {
        audioManager.SetVolume(volume);
    }
}
