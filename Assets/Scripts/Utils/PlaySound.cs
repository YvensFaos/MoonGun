using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySound : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }
}
