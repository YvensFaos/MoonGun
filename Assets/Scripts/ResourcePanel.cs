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

    [SerializeField] private float updateTextTime = 0.7f; 
    
    private readonly Vector3 _scaleFactor = new Vector3(2.08f, 1.05f, 1.05f);
    private readonly Vector3 _regularFactor = new Vector3(1.0f, 1.0f, 1.0f);
    public void UpdateAsteroidText(int asteroidsCount)
    {
        asteroidImage.DOScale(_scaleFactor, updateTextTime).OnComplete(()=>{asteroidImage.DOScale(_regularFactor, updateTextTime);});
        asteroidText.DOText(asteroidsCount.ToString(), updateTextTime, true, ScrambleMode.Numerals);
    }

    public void UpdateMineralText(int mineralCount)
    {
        mineralImage.DOScale(_scaleFactor, updateTextTime).OnComplete(()=>{mineralImage.DOScale(_regularFactor, updateTextTime);});
        mineralText.DOText(mineralCount.ToString(), updateTextTime, true, ScrambleMode.Numerals);
    }
}
