using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ShortDialogue : MonoBehaviour
{
    [SerializeField] private Text textToAnimate;
    [SerializeField] private float animateTimer;

    public void AnimateText(string text)
    {
        textToAnimate.text = "";
        textToAnimate.DOText(text, animateTimer);
    }
}
