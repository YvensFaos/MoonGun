using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CommandText : MonoBehaviour
{
    [SerializeField] private Text textToAnimate;
    [SerializeField] private float animateTimer;

    [SerializeField] private DialogueSequence dialogueSequence;
    private int _dialogueIndex;
    private bool _finished;
    
    public void ClearText()
    {
        textToAnimate.text = "";
    }

    public void SetText(string text)
    {
        ClearText();
        textToAnimate.DOText(text, animateTimer);
        _finished = false;
    }

    public void SetDialogueSequence(DialogueSequence dialogueSequence)
    {
        this.dialogueSequence = dialogueSequence;
        _dialogueIndex = 0;
    }

    public void StartSequence()
    {
        _dialogueIndex = 0;
        NextText();
    }

    public void NextText()
    {
        if (_dialogueIndex + 1 <= dialogueSequence.Length())
        {
            SetText(dialogueSequence[_dialogueIndex++]);    
        }
        else
        {
            _finished = true;
        }
    }

    public bool HasFinished => _finished;
}