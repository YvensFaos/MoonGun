using UnityEngine;
using UnityEngine.Events;

public class EnableDisableEventCaller : MonoBehaviour
{
    [SerializeField] private UnityEvent onEnableEvents;
    [SerializeField] private UnityEvent onDisableEvents;

    private void OnEnable()
    {
        onEnableEvents.Invoke();
    }

    private void OnDisable()
    {
        onDisableEvents.Invoke();
    }
}
