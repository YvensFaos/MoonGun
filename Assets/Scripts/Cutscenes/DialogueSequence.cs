using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSequence", menuName = "Dialogue Sequence", order = 2)]
public class DialogueSequence : ScriptableObject
{
    [TextArea]
    [SerializeField] private List<string> dialogueSequence;

    public string this[int key]
    {
        get => dialogueSequence[key];
        set => dialogueSequence[key] = value;
    }

    public int Length()
    {
        return dialogueSequence.Count;
    }
}
