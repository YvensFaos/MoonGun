using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioSlider : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void ChangeVolume(float value)
    {
        source.volume = value;
    }
    
    public void ChangeVolumeSlider(Slider slider)
    {
        ChangeVolume(slider.value);
    }
}
