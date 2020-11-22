using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CommandText : MonoBehaviour
{
    [SerializeField] private Text textToAnimate;
    [SerializeField] private float animateTimer;
    
    public void ClearText()
    {
        textToAnimate.text = "";
    }

    public void SetTimer(float timer)
    {
        animateTimer = timer;
    }

    public void SetText(string text)
    {
        ClearText();
        textToAnimate.DOText(text, animateTimer, true, ScrambleMode.Lowercase);
    }
}