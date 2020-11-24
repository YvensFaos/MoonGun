using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerAudioSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private string mixerChannel;

    public void SetMasterVolume(Slider volume){
        mixer.SetFloat (mixerChannel, volume.value);
    }
}
