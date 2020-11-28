using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ShortDialogue : MonoBehaviour
{
    [SerializeField] private Text textToAnimate;
    [SerializeField] private float animateTimer;
    
    [SerializeField] private bool playOnEnable;
    [TextArea, SerializeField] private string playOnEnableText;

    private void OnEnable()
    {
        if (playOnEnable)
        {
            AnimateText(playOnEnableText);
        }
    }

    public void AnimateText(string text)
    {
        textToAnimate.text = "";
        textToAnimate.DOText(text, animateTimer);
    }
}
