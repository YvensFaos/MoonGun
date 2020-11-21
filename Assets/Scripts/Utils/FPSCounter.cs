using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class FPSCounter : MonoBehaviour
{
    [SerializeField]
    public Text text;
    
    [SerializeField]
    private int averageSample;
    private float[] _averageValues;
    private int _averageIndex;
    
    public void Awake()
    {
        text = GetComponent<Text>();
        _averageValues = new float[averageSample];
        _averageIndex = 0;
        _averageValues[_averageIndex] = 0;
    }

    public void Update()
    {
        _averageValues[_averageIndex] = Time.deltaTime;
        _averageIndex = (++_averageIndex) % averageSample;
        var fps = 1.0f / Time.deltaTime;
        var averageFps = 1.0f / (_averageValues.Sum(value => value) / averageSample);
        text.text = "FPS: " + fps.ToString("0.0") + " AVG: " + averageFps.ToString("0.0");
    }
}
