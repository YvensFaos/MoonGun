using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class ForceTimelinePlay : MonoBehaviour
{
    private PlayableDirector _playableDirector;

    public void Awake()
    {
        _playableDirector = GetComponent<PlayableDirector>();
    }

    public void Start()
    {
        _playableDirector.Play();
    }
}
