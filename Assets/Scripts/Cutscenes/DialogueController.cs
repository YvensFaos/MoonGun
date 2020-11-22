using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private CommandText commandText;
    [SerializeField] private DialogueSequence dialogueSequence;

    [SerializeField] private UnityEvent startEvent;
    [SerializeField] private UnityEvent finishEvent;
    
    public void StartDialogueSequence()
    {
        startEvent.Invoke();
        StartCoroutine(DialogueCoroutine());
    }

    private IEnumerator DialogueCoroutine()
    {
        commandText.SetDialogueSequence(dialogueSequence);
        commandText.StartSequence();
        yield return new WaitUntil(() => commandText.HasFinished);
        finishEvent.Invoke();
    }
}
