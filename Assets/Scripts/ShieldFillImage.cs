using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ShieldFillImage : MonoBehaviour
{
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        _image.fillAmount = GameLogic.Instance.Shield.NormalizedIntegrity();
    }
}
