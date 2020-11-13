using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ResourcePanel : MonoBehaviour
{
    [SerializeField]
    private RectTransform asteroidImage;
    [SerializeField]
    private Text asteroidText;

    [SerializeField] 
    private RectTransform mineralImage;
    [SerializeField]
    private Text mineralText;

    [SerializeField] private float updateTextTime = 0.2f; 
    
    private Vector3 scaleFactor = new Vector3(1.08f, 1.05f, 1.05f);
    private Vector3 regularFactor = new Vector3(1.0f, 1.0f, 1.0f);
    public void UpdateAsteroidText(int asteroidsCount)
    {
        asteroidImage.DOScale(scaleFactor, updateTextTime);
        asteroidText.DOText(asteroidsCount.ToString(), updateTextTime, true, ScrambleMode.Numerals);
        asteroidImage.DOScale(regularFactor, updateTextTime);
    }

    public void UpdateMineralText(int mineralCount)
    {
        mineralImage.DOScale(scaleFactor, updateTextTime);
        mineralText.DOText(mineralCount.ToString(), updateTextTime, true, ScrambleMode.Numerals);
        mineralImage.DOScale(regularFactor, updateTextTime);
    }
}
