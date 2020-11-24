using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioSlider : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private Slider slider;
    
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        slider.value = source.volume;
    }

    public void ChangeVolume(float value)
    {
        source.volume = value;
    }
}
