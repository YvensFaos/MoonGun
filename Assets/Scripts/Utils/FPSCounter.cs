using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class FPSCounter : MonoBehaviour
{
    public Text text;

    public void Awake()
    {
        text = GetComponent<Text>();
    }

    public void Update()
    {
        var fps = 1.0f / Time.deltaTime;
        text.text = fps.ToString("0.0");
    }
}
